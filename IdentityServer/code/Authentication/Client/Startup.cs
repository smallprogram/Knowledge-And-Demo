using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                // 检查cookie判断是否认证的方案
                config.DefaultAuthenticateScheme = "ClientCookie";
                // 当认证后生成的cookie方案
                config.DefaultSignInScheme = "ClientCookie";
                // 使用设置的方案去判断用户能做什么
                config.DefaultChallengeScheme = "OurServer";
            })
                .AddCookie("ClientCookie")
                .AddOAuth("OurServer", config =>
                {
                    config.ClientId = "client_id";
                    config.ClientSecret = "client_secret";
                    config.CallbackPath = "/oauth/callback";
                    config.AuthorizationEndpoint = "https://localhost:6021/oauth/authorize";
                    config.TokenEndpoint = "https://localhost:6021/oauth/token";

                    // 是否保存access_token默认为false
                    config.SaveTokens = true;

                    // 提取access_token(JWT)中的claims到httpcontext中，以便用于授权
                    config.Events = new OAuthEvents()
                    {
                        OnCreatingTicket = context =>
                        {
                            var access_token = context.AccessToken;
                            var base64payload = access_token.Split('.')[1];
                            var decodeBtyes = ConvertFromBase64String(base64payload);
                            var jsonPayload = Encoding.UTF8.GetString(decodeBtyes);
                            var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);
                            foreach (var claim in claims)
                            {
                                context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddControllersWithViews();



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            // 你是谁，身份认证
            app.UseAuthentication();

            // 是否被允许访问，授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }


        private static byte[] ConvertFromBase64String(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/'); ;
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return Convert.FromBase64String(working);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
