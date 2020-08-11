using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace IdentityExample
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("MemoryDb");
            });


            // 添加Identity支持，并将其与EF结合
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Home/Login";
            });

            //services.AddAuthentication("CookieAuth")
            //    .AddCookie("CookieAuth", config =>
            //    {
            //        config.Cookie.Name = "zhusir.Cookie";
            //        config.LoginPath = "/Home/Login";
            //    });

            var mailKitOptions = _configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config => config.UseMailKit(mailKitOptions));

            services.AddControllersWithViews();
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
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
