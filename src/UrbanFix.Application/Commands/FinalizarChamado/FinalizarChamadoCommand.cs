using UrbanFix.Core.Mediator;

namespace UrbanFix.Application.Commands.FinalizarChamado
{
    public record FinalizarChamadoCommand(Guid id) : IRequest<Guid>{}
}
