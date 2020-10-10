using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppRefreshToken.Client.Services
{
    public interface IRefreshTokenService
    {
        Task<string> TryRefreshToken();
    }
}
