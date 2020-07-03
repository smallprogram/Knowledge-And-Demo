using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using RESTfulApi.Api.Data;
using RESTfulApi.Api.Services;
using System;
using System.Linq;

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

            services.AddHttpCacheHeaders(expires =>
            {
                expires.MaxAge = 60;
                expires.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
            }, validation =>
            {
                validation.MustRevalidate = true;
            });

            services.AddResponseCaching();

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true; //如果请求的类型与服务器支持的类型不一致，返回406状态码
                // 老写法，过时的
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());  //添加application/xml请求的媒体类型的支持
                //options.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter()); //将application/xml设置为首选媒体媒体类型

                options.CacheProfiles.Add("120sCacheProfile", new CacheProfile { Duration = 120 });
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()                  // 同时添加请求和响应的对于xml媒体类型的支持
                .ConfigureApiBehaviorOptions(setupAction =>               
                {
                    // 添加自定义的错误类型
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

            services.Configure<MvcOptions>(config => 
            {
                var newtonSoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");

                //var newtonSoftJsonInputFormatter = config.InputFormatters.OfType<NewtonsoftJsonInputFormatter>()?.FirstOrDefault();
                //newtonSoftJsonInputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");
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

            // app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
