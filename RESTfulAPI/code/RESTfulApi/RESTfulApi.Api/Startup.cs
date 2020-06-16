using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RESTfulApi.Api.Data;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Services;

namespace RESTfulApi.Api
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
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true; //如果请求的类型与服务器支持的类型不一致，返回406状态码
                // 老写法，过时的
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());  //添加application/xml请求的媒体类型的支持
                //options.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter()); //将application/xml设置为首选媒体媒体类型
            }).AddXmlDataContractSerializerFormatters();  // 同时添加请求和响应的对于xml媒体类型的支持

            services.AddScoped<ICompanyRepositroy, CompanyRepository>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=AppDB.db");
            });

            //services.AddMvc();
            //services.AddMvcCore();
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
