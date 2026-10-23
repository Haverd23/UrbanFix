using Microsoft.EntityFrameworkCore;
using UrbanFix.Application.Commands.CriarChamado;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Interfaces;
using UrbanFix.Application.Queries.ListarChamados;
using UrbanFix.Application.Repository;
using UrbanFix.Core.Cache;
using UrbanFix.Core.Mediator;
using UrbanFix.Core.Pagination;
using UrbanFix.Data;
using UrbanFix.Data.Mediator;
using UrbanFix.Data.Queries;
using UrbanFix.Domain;

namespace UrbanFix.Api.Extensions
{

        public static class DependencyInjectionConfig
        {
            public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
            {

            services.AddDbContext<ChamadoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMediator, MediatorDispatcher>();

            services.AddScoped<IChamadoQueryRepository, ChamadoQueryRepository>();
            services.AddScoped<IRequestHandler<ListarChamadosQuery, PagedResult<ChamadoDTO>>,ListarChamadosQueryHandler>();
            services.AddScoped<IRequestHandler<CriarChamadoCommand, Guid>,CriarChamadoCommandHandler>();
            services.AddScoped<IChamadoRepository, ChamadoRepository>();
            services.AddHttpClient<ICepService, CepService>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            services.AddScoped<ICacheService, CacheService>();

            return services;
        }

        }
    }