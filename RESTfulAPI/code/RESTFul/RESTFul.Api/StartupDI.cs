using Microsoft.Extensions.DependencyInjection;
using RESTFul.Repositories.Implement;
using RESTFul.Repositories.Interface;
using RESTFul.Services.Implement;
using RESTFul.Services.Interface;

namespace RESTFul.Api
{
    public static class StartupDI
    {
        public static IServiceCollection _DI_Setting(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepositroy, CompanyRepository>();
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();


            return services;
        }
    }
}
