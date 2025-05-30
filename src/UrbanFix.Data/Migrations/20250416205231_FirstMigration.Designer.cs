﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrbanFix.Data;

#nullable disable

namespace UrbanFix.Data.Migrations
{
    [DbContext(typeof(ChamadoContext))]
    [Migration("20250416205231_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UrbanFix.Domain.Models.Chamado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Chamados", (string)null);
                });

            modelBuilder.Entity("UrbanFix.Domain.Models.Chamado", b =>
                {
                    b.OwnsOne("UrbanFix.Domain.Models.Chamado+Imagem", "ImagemBytes", b1 =>
                        {
                            b1.Property<Guid>("ChamadoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte[]>("Dados")
                                .IsRequired()
                                .HasColumnType("varbinary(max)")
                                .HasColumnName("Imagem");

                            b1.HasKey("ChamadoId");

                            b1.ToTable("Chamados");

                            b1.WithOwner()
                                .HasForeignKey("ChamadoId");
                        });

                    b.OwnsOne("UrbanFix.Domain.Models.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("ChamadoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Bairro");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Cidade");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Estado");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Logradouro");

                            b1.HasKey("ChamadoId");

                            b1.ToTable("Chamados");

                            b1.WithOwner()
                                .HasForeignKey("ChamadoId");
                        });

                    b.Navigation("Endereco");

                    b.Navigation("ImagemBytes");
                });
#pragma warning restore 612, 618
        }
    }
}
