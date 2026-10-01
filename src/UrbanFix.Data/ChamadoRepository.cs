using Microsoft.EntityFrameworkCore;
using UrbanFix.Domain;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data
{
    public class ChamadoRepository : IChamadoRepository
    {
        private readonly ChamadoContext _context;

        public ChamadoRepository(ChamadoContext context)
        {
            _context = context;
        }

        public async Task CriarChamado(Chamado chamado)
        {
            await _context.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chamado>> ListarChamados()
        {
            return await _context.Chamados.ToListAsync();
        }
    }
}
