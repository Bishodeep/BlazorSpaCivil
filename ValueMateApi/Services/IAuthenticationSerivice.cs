using ValueMateApi.Models;

namespace ValueMateApi.Services;

public interface IAuthenticationSerivice
{
    Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequestDto);
    Task<bool> RegisterAsync(RegisterDto model);
}