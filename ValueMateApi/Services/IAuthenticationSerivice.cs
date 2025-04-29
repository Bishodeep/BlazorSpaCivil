using ValueMateApi.Models;

namespace ValueMateApi.Services;

public interface IAuthenticationSerivice
{
    LoginResponse Authenticate(LoginRequest loginRequest);
}