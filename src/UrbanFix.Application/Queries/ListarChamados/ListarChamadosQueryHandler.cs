using UrbanFix.Application.DTOs;
using UrbanFix.Application.Repository;
using UrbanFix.Core.Mediator;
using UrbanFix.Core.Pagination;
using UrbanFix.Domain;

namespace UrbanFix.Application.Queries.ListarChamados
{
    public class ListarChamadosQueryHandler : IRequestHandler<ListarChamadosQuery,PagedResult<ChamadoDTO>>
    {
        private readonly IChamadoQueryRepository _repository;

        public ListarChamadosQueryHandler(IChamadoQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ChamadoDTO>> HandleAsync(ListarChamadosQuery query)
        {
            var chamados = await _repository.ListarAsync(query.Filtro);
            return chamados;           
        }
    }
}
