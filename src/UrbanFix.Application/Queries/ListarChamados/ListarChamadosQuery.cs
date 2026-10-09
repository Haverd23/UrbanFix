using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Application.DTOs;
using UrbanFix.Core.Mediator;
using UrbanFix.Core.Pagination;

namespace UrbanFix.Application.Queries.ListarChamados
{
    public class ListarChamadosQuery : IRequest<PagedResult<ChamadoDTO>>
    {
        public ChamadoFiltroDTO Filtro { get; }

        public ListarChamadosQuery(ChamadoFiltroDTO filtro)
        {
            Filtro = filtro;
        }

    }
}
