using BlazorAppRefreshToken.Client.Helper;
using BlazorAppRefreshToken.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorAppRefreshToken.Client.Services
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _api;
        private CurrentUser _currentUser;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        public CustomStateProvider(IAuthService api,HttpClient httpClient, ILocalStorageService localStorage)
        {
             _api = api;
            _httpClient = httpClient;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrWhiteSpace(token))
                    return _anonymous;

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
 
        }

        private async Task<CurrentUser> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated) 
                return _currentUser;

            _currentUser = await _api.CurrentUserInfo();

            return _currentUser;
        }
        public async Task Logout()
        {
            await _api.Logout();
            _currentUser = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task<AuthResponseDto> Login(LoginRequest loginParameters)
        {
           var result =  await _api.Login(loginParameters);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return result;
        }
        public async Task Register(RegisterRequest registerParameters)
        {
            await _api.Register(registerParameters);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
