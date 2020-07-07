using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RESTFul.Api
{
    public static class StartupExtensions
    {
        /// <summary>
        /// 使用Marvin.Cache设置应用程序缓存
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection _HttpCacheSetting(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expires =>
            {
                expires.MaxAge = 60;
                expires.CacheLocation = Marvin.Cache.Headers.CacheLocation.Public;
            }, validation =>
            {
                validation.MustRevalidate = true;
            });

            services.AddResponseCaching();

            return services;
        }

        /// <summary>
        /// 控制器相关设置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection _ControllersSetting(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true; //如果请求的类型与服务器支持的类型不一致，返回406状态码
            })
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //使用CamelCase驼峰序列化JSON
            })
            .AddXmlDataContractSerializerFormatters() //添加输入输出xml媒体类型支持
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

            //添加自定义的Hateoas媒体类型输出格式化器
            services.Configure<MvcOptions>(config =>
            {
                var newtonSoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");
            });

            return services;
        }


    }
}
