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
                options.ReturnHttpNotAcceptable = true; //�������������������֧�ֵ����Ͳ�һ�£�����406״̬��
                // ��д������ʱ��
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());  //���application/xml�����ý�����͵�֧��
                //options.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter()); //��application/xml����Ϊ��ѡý��ý������
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
                var newtonSoftJsonOutputFormatter = config.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");

                //if (newtonSoftJsonOutputFormatter != null)
                //{
                //    newtonSoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");
                //}
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
