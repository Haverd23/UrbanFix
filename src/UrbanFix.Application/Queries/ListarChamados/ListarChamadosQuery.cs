using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Application.DTOs;
using UrbanFix.Core.Mediator;

namespace UrbanFix.Application.Queries.ListarChamados
{
    public class ListarChamadosQuery : IRequest<IEnumerable<ChamadoDTO>>
    {
    }
}
