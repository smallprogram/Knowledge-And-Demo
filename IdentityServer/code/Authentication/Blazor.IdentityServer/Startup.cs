using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Blazor.IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
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

namespace Blazor.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
                //config.UseInMemoryDatabase("MemoryDb");
            });


            // 添加Identity支持，并将其与EF结合
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            // https://localhost:7001/.well-known/openid-configuration


            var assembly = typeof(Startup).Assembly.GetName().Name;

            var filePath = Path.Combine(_environment.ContentRootPath, "Idp4.pfx");
            var certificate = new X509Certificate2(filePath, "identityserver");

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                // IdentityServer4 的 Configuration内容，例如IdentityResource、ApiScope、ApiResouce、Client等
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(assembly));
                })
                // IdentityServer4 的临时操作数据，例如AccessToken、RefreshToken等
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(assembly));
                })
                .AddSigningCredential(certificate);

            var mailKitOptions = _configuration.GetSection("Email").Get<MailKitOptions>();
            services.AddMailKit(config => config.UseMailKit(mailKitOptions));

            services.AddHttpClient();
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DbInit(app).GetAwaiter();

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

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
        public async Task DbInit(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var identityContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            identityContext.Database.EnsureDeleted();
            //identityContext.Database.EnsureCreated();
            identityContext.Database.Migrate();
            if (!identityContext.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var user = new IdentityUser("zhusir");
                await userManager.CreateAsync(user, "zhusir");
                IEnumerable<Claim> claims = new List<Claim>
                    {
                        new Claim("role", "admin"),
                        new Claim("role.apione", "apioneadmin"),
                        new Claim("scope.claim", "apionecliam"),
                    };
                userManager.AddClaimsAsync(user, claims).GetAwaiter();
            }

            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var configurationDbcontext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            configurationDbcontext.Database.Migrate();

            if (!configurationDbcontext.Clients.Any())
            {
                foreach (var client in Configuration.GetClients())
                {
                    configurationDbcontext.Clients.Add(client.ToEntity());
                }
                configurationDbcontext.SaveChanges();
            }

            if (!configurationDbcontext.IdentityResources.Any())
            {
                foreach (var resource in Configuration.GetIdentityResources())
                {
                    configurationDbcontext.IdentityResources.Add(resource.ToEntity());
                }
                configurationDbcontext.SaveChanges();
            }

            if (!configurationDbcontext.ApiResources.Any())
            {
                foreach (var resource in Configuration.GetApis())
                {
                    configurationDbcontext.ApiResources.Add(resource.ToEntity());
                }
                configurationDbcontext.SaveChanges();
            }

            if (!configurationDbcontext.ApiScopes.Any())
            {
                foreach (var resource in Configuration.GetApiScopes())
                {
                    configurationDbcontext.ApiScopes.Add(resource.ToEntity());
                }
                configurationDbcontext.SaveChanges();
            }

        }
    }
}
