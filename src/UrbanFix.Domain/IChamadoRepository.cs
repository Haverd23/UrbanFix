
using UrbanFix.Domain.Models;

namespace UrbanFix.Domain
{
    public interface IChamadoRepository
    {
        Task CriarChamado(Chamado chamado);
        Task<Chamado?> ObterPorId(Guid id);

        Task FinalizarChamado();
        Task<IEnumerable<Chamado>> ListarChamados();
    }
}
