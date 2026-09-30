using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanFix.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "varchar(100)", maxLength: 8, nullable: true),
                    Numero = table.Column<string>(type: "varchar(100)", maxLength: 10, nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: true),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");
        }
    }
}
