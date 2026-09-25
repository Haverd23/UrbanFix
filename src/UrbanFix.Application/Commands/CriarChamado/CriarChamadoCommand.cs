using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Domain.Enums;

namespace UrbanFix.Application.Commands.CriarChamado
{
    public record CriarChamadoCommand(TipoDeProblema Tipo,string Descricao,string CEP,string Numero);
}
