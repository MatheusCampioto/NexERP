using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFilial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicaoPagamento",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "CondicaoPagamentoId",
                table: "Pedidos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Filiais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CNPJ = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InscricaoMunicipal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CNAE = table.Column<string>(type: "text", nullable: true),
                    CRT = table.Column<string>(type: "text", nullable: true),
                    Modelo = table.Column<string>(type: "text", nullable: true),
                    SerieDanfe = table.Column<string>(type: "text", nullable: true),
                    SimplesNacional = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CEP = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Endereco = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NumeroEnd = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    PessoaId = table.Column<int>(type: "integer", nullable: true),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filiais_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CondicaoPagamentoId",
                table: "Pedidos",
                column: "CondicaoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Filiais_PessoaId",
                table: "Filiais",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_CondicoesPagamento_CondicaoPagamentoId",
                table: "Pedidos",
                column: "CondicaoPagamentoId",
                principalTable: "CondicoesPagamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_CondicoesPagamento_CondicaoPagamentoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Filiais");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CondicaoPagamentoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CondicaoPagamentoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "CondicaoPagamento",
                table: "Pedidos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
