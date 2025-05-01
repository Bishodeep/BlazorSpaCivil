using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using ValueMateApi.Constants;
using ValueMateApi.Data.Entities;
using ValueMateApi.Models;

namespace ValueMateApi.Services;

public class AuthenticationService : IAuthenticationSerivice
{
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthenticationService(IConfiguration config,UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
        if (user == null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
        if (!result.Succeeded)
            return null;
        var roles = await _userManager.GetRolesAsync(user);


        var claims = new List<Claim>();
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return new LoginResponseDto(tokenString);
    }

    public async Task<bool> RegisterAsync(RegisterDto model)
    {
        var user = new ApplicationUser() 
            { 
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                PhoneNumber=model.Phone
            };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return false;

        await _userManager.AddToRoleAsync(user, ApplicationRoleConstants.User); // Default role
        return true;
    }
   
}

public class Users
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}