using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data.Mappings
{
    public class ChamadoMapping : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Tipo).
                HasConversion<int>().
                IsRequired();

            builder.Property(c => c.Descricao).
                HasMaxLength(500).
                IsRequired();

            builder.Property(c => c.DataCriacao).
                IsRequired();

            builder.Property(c => c.Status).
                HasConversion<int>().
                IsRequired();

            builder.OwnsOne(c => c.Endereco, endereco =>
            {
                endereco.Property(e => e.CEP).IsRequired().HasMaxLength(8).HasColumnName("CEP");
                endereco.Property(e => e.Numero).IsRequired().HasMaxLength(10).HasColumnName("Numero");
                endereco.Property(e => e.Logradouro).IsRequired().HasMaxLength(100).HasColumnName("Logradouro");
                endereco.Property(e => e.Bairro).IsRequired().HasMaxLength(50).HasColumnName("Bairro");
                endereco.Property(e => e.Cidade).IsRequired().HasMaxLength(50).HasColumnName("Cidade");
                endereco.Property(e => e.Estado).IsRequired().HasMaxLength(50).HasColumnName("Estado");

            });

            builder.ToTable("Chamados");
        }
    }
}
