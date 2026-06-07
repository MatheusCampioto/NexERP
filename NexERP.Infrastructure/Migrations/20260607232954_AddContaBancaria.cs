using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContaBancaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContaBancariaId",
                table: "LancamentosFinanceiros",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormaPagamento",
                table: "LancamentosFinanceiros",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrupoParcela",
                table: "LancamentosFinanceiros",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroParcela",
                table: "LancamentosFinanceiros",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalParcelas",
                table: "LancamentosFinanceiros",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContasBancarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Banco = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Agencia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    NumeroConta = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SaldoInicial = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SaldoAtual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasBancarias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LancamentosFinanceiros_ContaBancariaId",
                table: "LancamentosFinanceiros",
                column: "ContaBancariaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LancamentosFinanceiros_ContasBancarias_ContaBancariaId",
                table: "LancamentosFinanceiros",
                column: "ContaBancariaId",
                principalTable: "ContasBancarias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LancamentosFinanceiros_ContasBancarias_ContaBancariaId",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropTable(
                name: "ContasBancarias");

            migrationBuilder.DropIndex(
                name: "IX_LancamentosFinanceiros_ContaBancariaId",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "ContaBancariaId",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "GrupoParcela",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "NumeroParcela",
                table: "LancamentosFinanceiros");

            migrationBuilder.DropColumn(
                name: "TotalParcelas",
                table: "LancamentosFinanceiros");
        }
    }
}
