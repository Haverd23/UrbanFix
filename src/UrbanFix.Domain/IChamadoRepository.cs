
using UrbanFix.Domain.Models;

namespace UrbanFix.Domain
{
    public interface IChamadoRepository
    {
        Task CriarChamado(Chamado chamado);
    }
}
