using JaggaKittaApp.Models;

namespace JaggaKittaApp.Services;

public interface IAuthenticationService
{
   Task<bool> Authenticate(LoginModel loginModel); 
}