using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Application.DTOs;
using UrbanFix.Core.Pagination;

namespace UrbanFix.Application.Repository
{
    public interface IChamadoQueryRepository
    {
        Task<PagedResult<ChamadoDTO>> ListarAsync(ChamadoFiltroDTO filtro);
    }
}
