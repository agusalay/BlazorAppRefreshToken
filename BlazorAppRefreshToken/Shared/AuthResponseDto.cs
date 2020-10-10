using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAppRefreshToken.Shared
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

