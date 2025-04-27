using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Populacao.Migrations
{
    /// <inheritdoc />
    public partial class Pessoas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "generos",
                columns: table => new
                {
                    GeneroId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "paises",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paises", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "pessoas",
                columns: table => new
                {
                    PessoaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeCompleto = table.Column<string>(type: "text", nullable: false),
                    idade = table.Column<int>(type: "integer", nullable: false),
                    GeneroId = table.Column<int>(type: "integer", nullable: false),
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoas", x => x.PessoaId);
                    table.ForeignKey(
                        name: "FK_pessoas_generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pessoas_paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "paises",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "generos",
                columns: new[] { "GeneroId", "Nome" },
                values: new object[,]
                {
                    { 1, "Masculino" },
                    { 2, "Feminino" }
                });

            migrationBuilder.InsertData(
                table: "paises",
                columns: new[] { "PaisId", "Nome" },
                values: new object[,]
                {
                    { 1, "Brasil" },
                    { 2, "Estados Unidos" },
                    { 3, "Argentina" },
                    { 4, "Portugal" },
                    { 5, "França" },
                    { 6, "Alemanha" },
                    { 7, "Japão" },
                    { 8, "Itália" },
                    { 9, "Reino Unido" },
                    { 10, "Canadá" },
                    { 11, "México" },
                    { 12, "China" },
                    { 13, "Índia" },
                    { 14, "Egito" },
                    { 15, "África do Sul" },
                    { 16, "Rússia" },
                    { 17, "Austrália" },
                    { 18, "Espanha" },
                    { 19, "Paquistão" },
                    { 20, "Indonésia" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_pessoas_GeneroId",
                table: "pessoas",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_pessoas_PaisId",
                table: "pessoas",
                column: "PaisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pessoas");

            migrationBuilder.DropTable(
                name: "generos");

            migrationBuilder.DropTable(
                name: "paises");
        }
    }
}
