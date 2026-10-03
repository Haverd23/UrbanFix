using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Core.Mediator;
using UrbanFix.Domain.Enums;

namespace UrbanFix.Application.Commands.CriarChamado
{
    public record CriarChamadoCommand(int Tipo,string Descricao,string CEP,string Numero): IRequest<Guid> { }
}
