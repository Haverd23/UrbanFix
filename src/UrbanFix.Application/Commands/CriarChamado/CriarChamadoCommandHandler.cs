using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Application.Interfaces;
using UrbanFix.Domain;
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

        public async Task HandleAsync(CriarChamadoCommand command)
        {
            var chamado = new Chamado(command.Tipo, command.Descricao);
            var enderecoDTO = await _cepService.ObterEnderecoPorCEPAsync(command.CEP);
            var endereco = new Endereco(enderecoDTO.Cep, command.Numero,
                enderecoDTO.Logradouro, enderecoDTO.Bairro, enderecoDTO.Cidade, enderecoDTO.Estado);

            chamado.DefinirEndereco(endereco);

            await _repository.CriarChamado(chamado);
        }

    }
}
