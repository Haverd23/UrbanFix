using Microsoft.EntityFrameworkCore;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data
{
    public class ChamadoContext : DbContext
    {
        public ChamadoContext(DbContextOptions<ChamadoContext> options) : base(options) { }

        public DbSet<Chamado> Chamados { get; set; }

    }
}
