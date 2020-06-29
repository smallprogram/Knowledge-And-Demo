using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
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
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                // 同时添加请求和响应的对于xml媒体类型的支持
                .AddXmlDataContractSerializerFormatters()
                
                // 添加自定义的错误类型
                // 默认的错误
                //{
                //    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                //    "title": "One or more validation errors occurred.",
                //    "status": 400,
                //    "traceId": "|e6c880f1-41183333b665f87b.",
                //    "errors": {
                //                "EmployeeAddDto": [
                //                    "员工编号不可以等于名",
                //            "姓和名不能一样"
                //        ]
                //    }
                //}
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var probleDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.google.com",
                            Title = "有错误！！",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "请参考详细信息",
                            Instance = context.HttpContext.Request.Path
                        };

                        probleDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(probleDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });
            //{
            //    "type": "http://www.google.com",
            //    "title": "有错误！！",
            //    "status": 422,
            //    "detail": "请参考详细信息",
            //    "instance": "/api/companies/e2f039ad-237c-4efe-97e9-15deccda6691/employees",
            //    "traceId": "0HM0KSAVHTPEC:00000001",
            //    "errors": {
            //                    "EmployeeAddDto": [
            //                        "员工编号必须和名不一样",
            //            "姓和名不能一样"
            //        ]
            //    }
            //}


            services.Configure<MvcOptions>(config => 
            {
                var newtonSoftJsonOutputFormatter = config.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                if (newtonSoftJsonOutputFormatter != null)
                {
                    newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");
                }
            });


            services.AddScoped<ICompanyRepositroy, CompanyRepository>();

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=AppDB.db");
            });

            //services.AddMvc();
            //services.AddMvcCore();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error!");
                        // 记录一下错误日志
                    });
                });
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
