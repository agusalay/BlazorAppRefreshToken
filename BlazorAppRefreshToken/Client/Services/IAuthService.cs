using BlazorAppRefreshToken.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppRefreshToken.Client.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);

        Task Logout();

        Task<CurrentUser> CurrentUserInfo();

        Task<string> RefreshToken();
    }
}
