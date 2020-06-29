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
                // ͬʱ����������Ӧ�Ķ���xmlý�����͵�֧��
                .AddXmlDataContractSerializerFormatters()
                
                // ����Զ���Ĵ�������
                // Ĭ�ϵĴ���
                //{
                //    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                //    "title": "One or more validation errors occurred.",
                //    "status": 400,
                //    "traceId": "|e6c880f1-41183333b665f87b.",
                //    "errors": {
                //                "EmployeeAddDto": [
                //                    "Ա����Ų����Ե�����",
                //            "�պ�������һ��"
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
            //{
            //    "type": "http://www.google.com",
            //    "title": "�д��󣡣�",
            //    "status": 422,
            //    "detail": "��ο���ϸ��Ϣ",
            //    "instance": "/api/companies/e2f039ad-237c-4efe-97e9-15deccda6691/employees",
            //    "traceId": "0HM0KSAVHTPEC:00000001",
            //    "errors": {
            //                    "EmployeeAddDto": [
            //                        "Ա����ű��������һ��",
            //            "�պ�������һ��"
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
