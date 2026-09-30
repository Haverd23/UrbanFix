using Microsoft.EntityFrameworkCore;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data
{
    public class ChamadoContext : DbContext
    {
        public ChamadoContext(DbContextOptions<ChamadoContext> options) : base(options) { }

        public DbSet<Chamado> Chamados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChamadoContext).Assembly);
        }

    }
}
