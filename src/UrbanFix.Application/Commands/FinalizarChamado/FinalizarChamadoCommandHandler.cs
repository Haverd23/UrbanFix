using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Core.Mediator;
using UrbanFix.Domain;

namespace UrbanFix.Application.Commands.FinalizarChamado
{
    public class FinalizarChamadoCommandHandler : IRequestHandler<FinalizarChamadoCommand, Guid>
    {
        private readonly IChamadoRepository _repository;

        public FinalizarChamadoCommandHandler(IChamadoRepository repository)
        {
            _repository = repository;
        }

        public  async Task<Guid> HandleAsync(FinalizarChamadoCommand command)
        {
            var chamado = await _repository.ObterPorId(command.id);
            chamado.FinalizarChamado();
            await _repository.FinalizarChamado();

            return chamado.Id;

        }
    }
}
