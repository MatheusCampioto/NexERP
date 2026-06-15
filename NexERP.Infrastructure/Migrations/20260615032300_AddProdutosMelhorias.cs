using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProdutosMelhorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AliquotaCOFINS",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AliquotaICMS",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AliquotaIPI",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AliquotaPIS",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Altura",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEST",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CFOP",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CSOSN",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CST_COFINS",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CST_ICMS",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CST_PIS",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Comprimento",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ControlaValidade",
                table: "Produtos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DiasValidade",
                table: "Produtos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstoqueMaximo",
                table: "Produtos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "Produtos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Largura",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalizacaoEstoque",
                table: "Produtos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NCM",
                table: "Produtos",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrigemMercadoria",
                table: "Produtos",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoBruto",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoLiquido",
                table: "Produtos",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoMinimo",
                table: "Produtos",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Pessoas_FornecedorId",
                table: "Produtos",
                column: "FornecedorId",
                principalTable: "Pessoas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Pessoas_FornecedorId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "AliquotaCOFINS",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "AliquotaICMS",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "AliquotaIPI",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "AliquotaPIS",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CEST",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CFOP",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CSOSN",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CST_COFINS",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CST_ICMS",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CST_PIS",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Comprimento",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ControlaValidade",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DiasValidade",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "EstoqueMaximo",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Largura",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "LocalizacaoEstoque",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "NCM",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "OrigemMercadoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PesoBruto",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PesoLiquido",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PrecoMinimo",
                table: "Produtos");
        }
    }
}
