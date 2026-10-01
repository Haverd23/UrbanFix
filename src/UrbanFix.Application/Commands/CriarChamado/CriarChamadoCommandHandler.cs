using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Application.Interfaces;
using UrbanFix.Domain;
using UrbanFix.Domain.Enums;
using UrbanFix.Domain.Models;

namespace UrbanFix.Application.Commands.CriarChamado
{
    public class CriarChamadoCommandHandler
    {
        private readonly IChamadoRepository _repository;
        private readonly ICepService _cepService;

        public CriarChamadoCommandHandler(IChamadoRepository repository, ICepService cepService)
        {
            _repository = repository;
            _cepService = cepService;
        }

        public async Task<Guid> HandleAsync(CriarChamadoCommand command)
        {
            if (!Enum.IsDefined(typeof(TipoDeProblema), command.Tipo))
                throw new ArgumentException("Tipo de problema inválido.");

            var tipo = (TipoDeProblema)command.Tipo;

            var chamado = new Chamado(tipo, command.Descricao);
            var enderecoDTO = await _cepService.ObterEnderecoPorCEPAsync(command.CEP);
            var endereco = new Endereco(enderecoDTO.Cep, command.Numero,
                enderecoDTO.Logradouro, enderecoDTO.Bairro, enderecoDTO.Cidade, enderecoDTO.Estado);

            chamado.DefinirEndereco(endereco);

            await _repository.CriarChamado(chamado);
            return chamado.Id;
        }

    }
}
