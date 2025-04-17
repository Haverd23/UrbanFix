using UrbanFix.Application.Services;
using UrbanFix.Application.Services.Interfaces;
using UrbanFix.Data;
using UrbanFix.Domain;

namespace UrbanFix.WebApi.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IChamadoAppService, ChamadoAppService>();
            services.AddScoped<IChamadoRepository, ChamadoRepository>();
            services.AddScoped<ChamadoContext>();
            services.AddScoped<ICepService, ViaCepService>();
            services.AddHttpClient();

            return services;

        }
    }
}
