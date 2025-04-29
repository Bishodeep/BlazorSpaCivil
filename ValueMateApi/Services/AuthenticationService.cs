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

    private readonly List<(string Username, string Password, string Role)> _users = new()
    {
        ("Bishodeep", "admin123", "Admin"),
        ("NoramlUser", "user123", "User")
    };

    public AuthenticationService(IConfiguration config)
    {
        _config = config;
    }


    public LoginResponse Authenticate(LoginRequest loginRequest)
    {
        var user = _users.FirstOrDefault(u =>
            u.Username == loginRequest.Email && u.Password == loginRequest.Password);

        if (user == default) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
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