using JaggaKittaApp.Models;

namespace JaggaKittaApp.Services;

public interface IAuthenticationService
{
   Task<bool> AuthenticateAsync(LoginModel loginModel); 
   Task<bool> RegisterAsync(RegisterModel registerModel); 
}