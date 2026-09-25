using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data
{
    public class ChamadoContext : DbContext
    {
        public ChamadoContext(DbContextOptions<ChamadoContext> options) : base(options) { }

        public DbSet<Chamado> Chamados { get; set; }

    }
}
