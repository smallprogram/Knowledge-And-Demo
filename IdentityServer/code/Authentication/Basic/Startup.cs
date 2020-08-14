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
                // 全局的授权策略
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
                // 基本策略用法
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //.RequireAuthenticatedUser()
                //.RequireClaim(ClaimTypes.DateOfBirth)
                //.Build();
                //configure.DefaultPolicy = defaultAuthPolicy;

                // 自定义策略用法
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


            // 自定义的授权处理器
            services.AddScoped<IAuthorizationHandler, CostomRequeireClaimHandler>();

            // 自定义的针对资源进行授权
            services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();

            // 自定义的运行时填充Claim
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

            // 你是谁，身份认证
            app.UseAuthentication();

            // 是否被允许访问，授权
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
