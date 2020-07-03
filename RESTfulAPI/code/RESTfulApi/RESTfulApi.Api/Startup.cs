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
                options.ReturnHttpNotAcceptable = true; //�������������������֧�ֵ����Ͳ�һ�£�����406״̬��
                // ��д������ʱ��
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());  //���application/xml�����ý�����͵�֧��
                //options.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter()); //��application/xml����Ϊ��ѡý��ý������

                options.CacheProfiles.Add("120sCacheProfile", new CacheProfile { Duration = 120 });
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()                  // ͬʱ����������Ӧ�Ķ���xmlý�����͵�֧��
                .ConfigureApiBehaviorOptions(setupAction =>               
                {
                    // ����Զ���Ĵ�������
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var probleDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "http://www.google.com",
                            Title = "�д��󣡣�",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "��ο���ϸ��Ϣ",
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
                        // ��¼һ�´�����־
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
