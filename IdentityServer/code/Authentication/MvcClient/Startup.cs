using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "MvcClientCookie";
                config.DefaultChallengeScheme = "oidc";
                
            })
                .AddCookie("MvcClientCookie")
                .AddOpenIdConnect("oidc", config =>
                {
                    config.Authority = "https://localhost:7001";
                    config.ClientId = "client_id_mvc";
                    config.ClientSecret = "client_secret_mvc";
                    config.SaveTokens = true;

                    config.ResponseType = "code"; // ����oidcģʽ

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

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
