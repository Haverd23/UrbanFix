using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data.Mappings
{
    public class ChamadoMapping : IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CEP)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(3000);

            builder.Property(c => c.DataCriacao)
                .IsRequired();

            builder.OwnsOne(c => c.ImagemBytes, img =>
            {
                img.Property(i => i.Dados)
                    .HasColumnName("Imagem")
                    .HasColumnType("varbinary(max)");
            });

            builder.OwnsOne<Endereco>("Endereco", endereco =>
            {
                endereco.Property(e => e.Logradouro)
                    .HasColumnName("Logradouro")
                    .HasMaxLength(200);

                endereco.Property(e => e.Bairro)
                    .HasColumnName("Bairro")
                    .HasMaxLength(100);

                endereco.Property(e => e.Cidade)
                    .HasColumnName("Cidade")
                    .HasMaxLength(100);

                endereco.Property(e => e.Estado)
                    .HasColumnName("Estado")
                    .HasMaxLength(50);
            });

            builder.ToTable("Chamados");
        }
    }
}