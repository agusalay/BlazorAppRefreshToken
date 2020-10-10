using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorAppRefreshToken.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace BlazorAppRefreshToken.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddOptions();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<CustomStateProvider>();

            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped(sp => 
            new HttpClient { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
            }.EnableIntercept(sp));

            builder.Services.AddHttpClientInterceptor();

            builder.Services.AddScoped<IRefreshTokenService,RefreshTokenService>();

            builder.Services.AddScoped<IHttpInterceptorService, HttpInterceptorService>();

            await builder.Build().RunAsync();
        }
    }
}
