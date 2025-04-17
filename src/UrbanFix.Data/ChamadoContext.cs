using Microsoft.EntityFrameworkCore;
using UrbanFix.Core.Data;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data
{
    public class ChamadoContext : DbContext, IUnitOfWork
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

        public async Task<bool> Commit()
        {
            try
            {
                var result = await base.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar as mudanças no banco de dados", ex);
            }
        }
    }
}
