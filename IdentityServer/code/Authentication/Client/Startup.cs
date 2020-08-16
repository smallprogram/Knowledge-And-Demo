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
                // ���cookie�ж��Ƿ���֤�ķ���
                config.DefaultAuthenticateScheme = "ClientCookie";
                // ����֤�����ɵ�cookie����
                config.DefaultSignInScheme = "ClientCookie";
                // ʹ�����õķ���ȥ�ж��û�����ʲô
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

                    // �Ƿ񱣴�access_tokenĬ��Ϊfalse
                    config.SaveTokens = true;

                    // ��ȡaccess_token(JWT)�е�claims��httpcontext�У��Ա�������Ȩ
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

            // ����˭�������֤
            app.UseAuthentication();

            // �Ƿ�������ʣ���Ȩ
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
