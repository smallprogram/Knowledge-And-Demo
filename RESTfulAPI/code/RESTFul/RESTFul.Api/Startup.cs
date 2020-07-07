using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RESTFul.Data.DatabaseContext;
using RESTFul.MappingProfiles.Helper;

namespace RESTFul.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services._HttpCacheSetting();

            services._ControllersSetting();

            services._DI_Setting();


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=AppDB.db");
            });

            // ×¢²áAutoMapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfilesAssembly))); //AppDomain.CurrentDomain.GetAssemblies()
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
