using UrbanFix.Application.Commands.CriarChamado;
using UrbanFix.Application.DTOs;
using UrbanFix.Domain;

namespace UrbanFix.Application.Queries.ListarChamados
{
    public class ListarChamadosQueryHandler
    {
        private readonly IChamadoRepository _repository;

        public ListarChamadosQueryHandler(IChamadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ChamadoDTO>> HandleAsync()
        {
            var chamados = await _repository.ListarChamados();

            return chamados.Select(chamado => new ChamadoDTO
            {
                Id = chamado.Id,
                Tipo = chamado.Tipo.ToString(),
                Descricao = chamado.Descricao,
                DataCriacao = chamado.DataCriacao,
                Status = chamado.Status.ToString(),
                Cep = chamado.Endereco.CEP,
                Numero = chamado.Endereco.Numero,
                Logradouro = chamado.Endereco.Logradouro,
                Bairro = chamado.Endereco.Bairro,
                Cidade = chamado.Endereco.Cidade,
                Estado = chamado.Endereco.Estado
            });
        }



    }
}
