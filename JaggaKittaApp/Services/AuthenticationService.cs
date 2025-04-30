using System.Net.Http.Json;
using JaggaKittaApp.Authorization;
using JaggaKittaApp.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace JaggaKittaApp.Services;

public class AuthenticationService:IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly UserAuthStateProvider _authenticationStateProvider;
    public AuthenticationService(HttpClient client, UserAuthStateProvider authenticationStateProvider)
    {
        _client = client;
        _authenticationStateProvider= authenticationStateProvider;
    }
    public async Task<bool> Authenticate(LoginModel loginModel)
    {
        var response = await _client.PostAsJsonAsync("/login", loginModel);
        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            _authenticationStateProvider.SetToken(tokenResponse.Token);
            return true;
        }
        return false;
    }
}