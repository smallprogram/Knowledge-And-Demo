using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ApiOne
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    //config.MetadataAddress 
                    config.Authority = "https://localhost:7001";
                    config.Audience = "ApiOne";

                    //config.TokenValidationParameters.ValidateAudience = false; //不验证aud

                    // 验证aud暂时不可用，ValidTypes不可用 https://github.com/IdentityServer/IdentityServer4/issues/4515
                    //config.Audience = "https://localhost:7002";
                    //config.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };



                });

            services.AddControllers();
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
                endpoints.MapControllers();
            });
        }
    }
}
