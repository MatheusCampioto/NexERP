using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioPermissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcessoEstoque",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoFinanceiro",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoPedidos",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoPessoas",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoProdutos",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoRelatorios",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcessoUsuarios",
                table: "Usuarios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoAcesso",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcessoEstoque",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AcessoFinanceiro",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AcessoPedidos",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AcessoPessoas",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AcessoProdutos",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AcessoRelatorios",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AcessoUsuarios",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UltimoAcesso",
                table: "Usuarios");
        }
    }
}
