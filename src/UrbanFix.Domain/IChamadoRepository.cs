using UrbanFix.Core.Data;
using UrbanFix.Domain.Models;

namespace UrbanFix.Domain
{
    public interface IChamadoRepository : IRepository<Chamado>
    {
        Task<Chamado> ObterPorIdAsync(Guid id);
        void Remover(Chamado chamado);
        Task Adicionar(Chamado chamado);
        Task AtualizarStatusAsync(Guid chamadoId, Chamado.TipoDeStatus novoStatus);
        Task<IEnumerable<Chamado>> ObterTodosAsync();
        Task<IEnumerable<Chamado>> ObterPorStatusAsync(Chamado.TipoDeStatus status);
        Task<IEnumerable<Chamado>> ObterPorTipoAsync(Chamado.TipoDeProblema tipo);
        Task<IEnumerable<Chamado>> ObterPorStatusMaisAntigosAsync(Chamado.TipoDeStatus status);
        Task<IEnumerable<Chamado>> ObterPorTipoMaisAntigosAsync(Chamado.TipoDeProblema tipo);
    }
}
