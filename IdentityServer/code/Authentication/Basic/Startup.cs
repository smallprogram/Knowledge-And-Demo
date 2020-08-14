using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Basic.AuthorizationRequirements;
using Basic.Controllers;
using Basic.CustomPolicyProvider;
using Basic.Transformer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basic
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllersWithViews(config =>
            {
                // ȫ�ֵ���Ȩ����
                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder
                .RequireAuthenticatedUser()
                .Build();

                config.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));
            });


            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "zhusir.Cookie";
                    config.LoginPath = "/Home/Authenticate";
                    config.AccessDeniedPath = "/Home/AccessDenied";
                });


            services.AddAuthorization(configure =>
            {
                // ���������÷�
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //.RequireAuthenticatedUser()
                //.RequireClaim(ClaimTypes.DateOfBirth)
                //.Build();
                //configure.DefaultPolicy = defaultAuthPolicy;

                // �Զ�������÷�
                //configure.AddPolicy("Claim.custom", policyBuilder =>
                // {
                //     policyBuilder.RequireClaim(ClaimTypes.DateOfBirth);
                // });

                //configure.AddPolicy("Claim.custom", policyBuilder =>
                //{
                //    policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
                //});

                configure.AddPolicy("Claim.custom", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });
            });

            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizaitonPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, SecurityLevelHandler>();


            // �Զ������Ȩ������
            services.AddScoped<IAuthorizationHandler, CostomRequeireClaimHandler>();

            // �Զ���������Դ������Ȩ
            services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();

            // �Զ��������ʱ���Claim
            services.AddScoped<IClaimsTransformation, ClaimsTransformation>();

            services.AddRazorPages()
                .AddRazorPagesOptions(config =>
                {
                    // root dir is /Pages/
                    config.Conventions.AllowAnonymousToPage("/Razor/Index");
                    config.Conventions.AuthorizePage("/Razor/Secured");
                    config.Conventions.AuthorizePage("/Razor/Policy", "Claim.custom");
                    config.Conventions.AuthorizeFolder("/RzoreSecured");
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            // ����˭��������֤
            app.UseAuthentication();

            // �Ƿ��������ʣ���Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}