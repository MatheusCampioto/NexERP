using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPessoasMelhorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF_CNPJ",
                table: "Pessoas",
                newName: "RG");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Pessoas",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Pessoas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Pessoas",
                type: "character varying(18)",
                maxLength: 18,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Pessoas",
                type: "character varying(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Pessoas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Pessoas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Pessoas",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoCivil",
                table: "Pessoas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Funcao",
                table: "Pessoas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                table: "Pessoas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoMunicipal",
                table: "Pessoas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeContato",
                table: "Pessoas",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                table: "Pessoas",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Pessoas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Pessoas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profissao",
                table: "Pessoas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Pessoas",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Pessoas",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDocumento",
                table: "Pessoas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Celular",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "EstadoCivil",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Funcao",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "InscricaoMunicipal",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "NomeContato",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Profissao",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "Pessoas");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "Pessoas",
                newName: "CPF_CNPJ");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Pessoas",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
