using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using ValueMateApi.Models;
using LoginRequest = ValueMateApi.Models.LoginRequest;
using LoginResponse = ValueMateApi.Models.LoginResponse;

namespace ValueMateApi.Services;

public class AuthenticationService : IAuthenticationSerivice
{
    private readonly IConfiguration _config;

    private List<Users> _users = new ()
    {
        new Users
            {
                Email="Bishodeep@gmail.com",
                Password="admin123", 
                Role="Admin"
            },
        new Users
        {
            Email="NoramlUser@gmail.com",
            Password="user123",
            Role="User"
        }
        
    };

    public AuthenticationService(IConfiguration config)
    {
        _config = config;
    }


    public LoginResponse Authenticate(LoginRequest loginRequest)
    {
        var user = _users.FirstOrDefault(u =>
            u.Email.Trim().ToUpper() == loginRequest.Email.Trim().ToUpper() && u.Password.Trim().ToUpper() == loginRequest.Password.Trim().ToUpper());

        if (user == null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return new LoginResponse(tokenString);
    }

   
}

public class Users
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}