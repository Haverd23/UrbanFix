using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanFix.Application.DTOs;
using UrbanFix.Domain.Models;

namespace UrbanFix.Application.Services.Interfaces
{
    public interface IChamadoAppService
    {
        Task AdicionarAsync(CriarChamadoDTO dto);
        Task AtualizarStatusAsync(Guid chamadoId, Chamado.TipoDeStatus novoStatus);
        Task RemoverAsync(Guid chamadoId);

        Task<ChamadoDTO> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ChamadoDTO>> ObterTodosAsync();
        Task<IEnumerable<ChamadoDTO>> ObterPorStatusAsync(Chamado.TipoDeStatus status);
        Task<IEnumerable<ChamadoDTO>> ObterPorTipoAsync(Chamado.TipoDeProblema tipo);
        Task<IEnumerable<ChamadoDTO>> ObterPorStatusMaisAntigosAsync(Chamado.TipoDeStatus status);
        Task<IEnumerable<ChamadoDTO>> ObterPorTipoMaisAntigosAsync(Chamado.TipoDeProblema tipo);
    }
}