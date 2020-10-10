using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace BlazorAppRefreshToken.Client.Services
{
    public interface IHttpInterceptorService
    {
        void DisposeEvent();
        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);
        void RegisterEvent();
    }
}
