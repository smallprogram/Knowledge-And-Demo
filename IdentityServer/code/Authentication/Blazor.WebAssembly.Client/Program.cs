using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Blazor.WebAssembly.Client.ProgramExtend;

namespace Blazor.WebAssembly.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("LocalAPI",sp => sp.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) );

            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri("https://localhost:25001"))
                .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();


            builder.Services.AddOidcAuthentication(options =>
            {
                //builder.Configuration.Bind("Local", options.ProviderOptions);
                options.ProviderOptions.Authority = "https://localhost:25003";
                options.ProviderOptions.ClientId = "client_id_blazor";
                options.ProviderOptions.PostLogoutRedirectUri = "/";
                options.ProviderOptions.ResponseType = "code";

                options.ProviderOptions.DefaultScopes.Clear();
                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.DefaultScopes.Add("profile");
                //options.ProviderOptions.DefaultScopes.Add("ApiOne");
                options.ProviderOptions.DefaultScopes.Add("ApiOne.read");
            });



            await builder.Build().RunAsync();
        }
    }
}
