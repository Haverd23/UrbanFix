

using UrbanFix.Domain.Models;

namespace UrbanFix.Application.DTOs.Mapping
{
    public static class ChamadoMapper
    {
        public static ChamadoDTO ParaDTO(Chamado chamado)
        {
            return new ChamadoDTO
            {
                Descricao = chamado.Descricao,
                CEP = chamado.CEP,
                Numero = chamado.Numero,
                Tipo = chamado.Tipo.ToString(),
                Status = chamado.Status.ToString(),
                DataCriacao = chamado.DataCriacao,
                Endereco = chamado.Endereco != null
                    ? new EnderecoDTO
                    {
                        Logradouro = chamado.Endereco.Logradouro,
                        Bairro = chamado.Endereco.Bairro,
                        Cidade = chamado.Endereco.Cidade,
                        Estado = chamado.Endereco.Estado
                    }
                    : null
            };
        }

        public static IEnumerable<ChamadoDTO> ParaDTO(IEnumerable<Chamado> chamados)
        {
            return chamados.Select(ParaDTO);
        }
    }
}
