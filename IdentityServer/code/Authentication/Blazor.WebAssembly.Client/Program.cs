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

namespace Blazor.WebAssembly.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("HostIS.ServerAPI",
                    client => client.BaseAddress = new Uri("https://localhost:25003"))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("HostIS.ServerAPI"));

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }
    }
}
