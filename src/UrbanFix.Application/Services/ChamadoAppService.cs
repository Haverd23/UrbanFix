
using UrbanFix.Application.DTOs;
using UrbanFix.Application.DTOs.Mapping;
using UrbanFix.Application.Services.Interfaces;
using UrbanFix.Domain;
using UrbanFix.Domain.Models;

namespace UrbanFix.Application.Services
{
    public class ChamadoAppService : IChamadoAppService
    {
        private readonly IChamadoRepository _chamadoRepository;
        private readonly ICepService _cepService;

        public ChamadoAppService(IChamadoRepository chamadoRepository, ICepService cepService)
        {
            _chamadoRepository = chamadoRepository;
            _cepService = cepService;
        }

        public async Task AdicionarAsync(CriarChamadoDTO dto)
        {
            var chamado = new Chamado(
                tipo: (Chamado.TipoDeProblema)dto.Tipo,
                descricao: dto.Descricao,
                cep: dto.CEP,
                numero: dto.Numero
            );

            if (!string.IsNullOrWhiteSpace(dto.Base64Imagem))
            {
                chamado.AdicionarImagem(new Chamado.Imagem(dto.Base64Imagem));
            }

            var enderecoDto = await _cepService.ObterEnderecoPorCepAsync(dto.CEP);
            var endereco = new Endereco(
                logradouro: enderecoDto.Logradouro,
                bairro: enderecoDto.Bairro,
                cidade: enderecoDto.Cidade,
                estado: enderecoDto.Estado
            );

            chamado.DefinirEndereco(endereco);

            await _chamadoRepository.Adicionar(chamado);
            await _chamadoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarStatusAsync(Guid chamadoId, Chamado.TipoDeStatus novoStatus)
        {
            var chamado = await _chamadoRepository.ObterPorIdAsync(chamadoId);
            if (chamado == null) throw new Exception("Chamado não encontrado");

            chamado.AtualizarStatus(novoStatus);

            await _chamadoRepository.UnitOfWork.Commit();
        }

        public async Task RemoverAsync(Guid chamadoId)
        {
            var chamado = await _chamadoRepository.ObterPorIdAsync(chamadoId);
            if (chamado == null) throw new Exception("Chamado não encontrado");

            _chamadoRepository.Remover(chamado);

            await _chamadoRepository.UnitOfWork.Commit();
        }

        public async Task<ChamadoDTO> ObterPorIdAsync(Guid id)
        {
            var chamado = await _chamadoRepository.ObterPorIdAsync(id);
            if (chamado == null) throw new Exception("Chamado não encontrado");

            return ChamadoMapper.ParaDTO(chamado);
        }

        public async Task<IEnumerable<ChamadoDTO>> ObterTodosAsync()
        {
            var chamados = await _chamadoRepository.ObterTodosAsync();
            return ChamadoMapper.ParaDTO(chamados);
        }

        public async Task<IEnumerable<ChamadoDTO>> ObterPorStatusAsync(Chamado.TipoDeStatus status)
        {
            var chamados = await _chamadoRepository.ObterPorStatusAsync(status);
            return ChamadoMapper.ParaDTO(chamados);
        }

        public async Task<IEnumerable<ChamadoDTO>> ObterPorTipoAsync(Chamado.TipoDeProblema tipo)
        {
            var chamados = await _chamadoRepository.ObterPorTipoAsync(tipo);
            return ChamadoMapper.ParaDTO(chamados);
        }

        public async Task<IEnumerable<ChamadoDTO>> ObterPorStatusMaisAntigosAsync(Chamado.TipoDeStatus status)
        {
            var chamados = await _chamadoRepository.ObterPorStatusMaisAntigosAsync(status);
            return ChamadoMapper.ParaDTO(chamados);
        }

        public async Task<IEnumerable<ChamadoDTO>> ObterPorTipoMaisAntigosAsync(Chamado.TipoDeProblema tipo)
        {
            var chamados = await _chamadoRepository.ObterPorTipoMaisAntigosAsync(tipo);
            return ChamadoMapper.ParaDTO(chamados);
        }
    }
}