using Microsoft.EntityFrameworkCore;
using UrbanFix.Core.Data;
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

        public IUnitOfWork UnitOfWork => _context;

        public async Task Adicionar(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
        }

        public async Task AtualizarStatusAsync(Guid chamadoId, Chamado.TipoDeStatus novoStatus)
        {
            var chamado = await _context.Chamados.FindAsync(chamadoId);

            if (chamado == null) return;

            chamado.AtualizarStatus(novoStatus);

        }
        public async Task<Chamado> ObterPorIdAsync(Guid id)
        {
            return await _context.Chamados.FindAsync(id);

        }

        public async Task<IEnumerable<Chamado>> ObterPorStatusAsync(Chamado.TipoDeStatus status)
        {
            return await _context.Chamados
                    .Where(c => c.Status == status)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Chamado>> ObterPorStatusMaisAntigosAsync(Chamado.TipoDeStatus status)
        {
            return await _context.Chamados
                   .Where(c => c.Status == status)
                   .OrderBy(c => c.DataCriacao)
                   .ToListAsync();
        }

        public async Task<IEnumerable<Chamado>> ObterPorTipoAsync(Chamado.TipoDeProblema tipo)
        {
            return await _context.Chamados
                  .Where(c => c.Tipo == tipo)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Chamado>> ObterPorTipoMaisAntigosAsync(Chamado.TipoDeProblema tipo)
        {
            return await _context.Chamados
                   .Where(c => c.Tipo == tipo)
                   .OrderBy(c => c.DataCriacao)
                   .ToListAsync();
        }

        public async Task<IEnumerable<Chamado>> ObterTodosAsync()
        {
            return await _context.Chamados.ToListAsync();
        }

        public void Remover(Chamado chamado)
        {
            _context.Chamados.Remove(chamado);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
