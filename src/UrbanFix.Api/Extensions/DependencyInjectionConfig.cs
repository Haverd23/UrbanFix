using Microsoft.EntityFrameworkCore;
using UrbanFix.Application.Commands.CriarChamado;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Interfaces;
using UrbanFix.Application.Queries.ListarChamados;
using UrbanFix.Core.Mediator;
using UrbanFix.Data;
using UrbanFix.Data.Mediator;
using UrbanFix.Domain;

namespace UrbanFix.Api.Extensions
{

        public static class DependencyInjectionConfig
        {
            public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
            {

            services.AddDbContext<ChamadoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMediator, MediatorDispatcher>();

            services.AddScoped<IRequestHandler<ListarChamadosQuery, IEnumerable<ChamadoDTO>>,ListarChamadosQueryHandler>();
            services.AddScoped<IRequestHandler<CriarChamadoCommand, Guid>,CriarChamadoCommandHandler>();
            services.AddScoped<IChamadoRepository, ChamadoRepository>();
            services.AddHttpClient<ICepService, CepService>();

            return services;
        }

        }
    }