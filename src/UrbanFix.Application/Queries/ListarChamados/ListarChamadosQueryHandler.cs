using System.Text.Json;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Repository;
using UrbanFix.Core.Cache;
using UrbanFix.Core.Mediator;
using UrbanFix.Core.Pagination;
using UrbanFix.Domain;

namespace UrbanFix.Application.Queries.ListarChamados
{
    public class ListarChamadosQueryHandler : IRequestHandler<ListarChamadosQuery,PagedResult<ChamadoDTO>>
    {
        private readonly IChamadoQueryRepository _repository;
        private readonly ICacheService _cache;
        public ListarChamadosQueryHandler(IChamadoQueryRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<PagedResult<ChamadoDTO>> HandleAsync(ListarChamadosQuery query)
        {
            var filtroJson = JsonSerializer.Serialize(query.Filtro);
            var cacheKey = $"chamados:{filtroJson}";

            var chamadosCache = await _cache.GetCachedDataAsync<PagedResult<ChamadoDTO>>(cacheKey);

            if(chamadosCache!= null)
            {
                return chamadosCache;
            }

            var chamados = await _repository.ListarAsync(query.Filtro);

            await _cache.SetCachedDataAsync(cacheKey, chamados, TimeSpan.FromMinutes(10));
            return chamados;           
        }
    }
}
