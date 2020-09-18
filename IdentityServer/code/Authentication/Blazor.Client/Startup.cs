using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blazor.Client.Data;

namespace Blazor.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();


            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "BlazorClientCookie";
                config.DefaultChallengeScheme = "oidc";

            })
               .AddCookie("BlazorClientCookie")
               .AddOpenIdConnect("oidc", config =>
               {
                   config.Authority = "https://localhost:25003";
                   config.ClientId = "client_id_blazor";
                   config.ClientSecret = "client_secret_blazor";
                   config.SaveTokens = true;

                   config.ResponseType = "code"; // ����oidcģʽ

                   config.SignedOutCallbackPath = "/";

                   config.UsePkce = true;

                   //��Claim���в�����ɾ����ӳ�䵽�µ�Claim��
                   //config.ClaimActions.DeleteClaim("amr");
                   //config.ClaimActions.MapUniqueJsonKey("RawCoding.role", "role");

                   // ʹ��idp��������userinfo�˵������û���Ϣ
                   config.GetClaimsFromUserInfoEndpoint = true;

                   // �������scope���в�����ȥ�����õģ�ֻ������õ�scope
                   config.Scope.Clear();
                   config.Scope.Add("openid");
                   config.Scope.Add("profile");
                   config.Scope.Add("offline_access"); // refresh_token
                                                       //config.Scope.Add("role.scope");
                   config.Scope.Add("ApiOne.read");
                   config.Scope.Add("ApiTwo.read");

               });

            services.AddHttpClient();


            services.AddSingleton<WeatherForecastService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
