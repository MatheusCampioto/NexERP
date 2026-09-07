using FluentAssertions;
using NexERP.Domain.Entities;
using NexERP.Domain.Exceptions;

namespace NexERP.Tests.Domain;

public class PessoaTests
{
    [Fact]
    public void Criar_ComNomeVazio_DeveLancarDomainException()
    {
        Action acao = () => new Pessoa("", "CPF", "Cliente");

        acao.Should().Throw<DomainException>()
            .WithMessage("Nome é obrigatório.");
    }

    [Fact]
    public void Criar_ComTipoDocumentoInvalido_DeveLancarDomainException()
    {
        Action acao = () => new Pessoa("João", "RG", "Cliente");

        acao.Should().Throw<DomainException>()
            .WithMessage("TipoDocumento deve ser CPF ou CNPJ.");
    }

    [Fact]
    public void Criar_ComTipoInvalido_DeveLancarDomainException()
    {
        Action acao = () => new Pessoa("João", "CPF", "Desconhecido");

        acao.Should().Throw<DomainException>()
            .WithMessage("Tipo inválido: Desconhecido.");
    }

    [Fact]
    public void Criar_ComDadosValidos_DeveIniciarAtiva()
    {
        var pessoa = new Pessoa("João Silva", "CPF", "Cliente");

        pessoa.Ativo.Should().BeTrue();
        pessoa.Nome.Should().Be("João Silva");
        pessoa.TipoDocumento.Should().Be("CPF");
        pessoa.Tipo.Should().Be("Cliente");
    }

    [Fact]
    public void Validar_PFSemCPF_DeveLancarDomainException()
    {
        var pessoa = new Pessoa("João", "CPF", "Cliente");
        pessoa.CPF = null;

        Action acao = () => pessoa.Validar();

        acao.Should().Throw<DomainException>()
            .WithMessage("CPF é obrigatório para Pessoa Física.");
    }

    [Fact]
    public void Validar_PJSemCNPJ_DeveLancarDomainException()
    {
        var pessoa = new Pessoa("Empresa", "CNPJ", "Fornecedor");
        pessoa.RazaoSocial = "Empresa LTDA";
        pessoa.CNPJ = null;

        Action acao = () => pessoa.Validar();

        acao.Should().Throw<DomainException>()
            .WithMessage("CNPJ é obrigatório para Pessoa Jurídica.");
    }

    [Fact]
    public void Validar_PJSemRazaoSocial_DeveLancarDomainException()
    {
        var pessoa = new Pessoa("Empresa", "CNPJ", "Fornecedor");
        pessoa.CNPJ = "12.345.678/0001-99";
        pessoa.RazaoSocial = null;

        Action acao = () => pessoa.Validar();

        acao.Should().Throw<DomainException>()
            .WithMessage("Razão Social é obrigatória para Pessoa Jurídica.");
    }

    [Fact]
    public void Desativar_PessoaAtiva_DeveDesativar()
    {
        var pessoa = new Pessoa("João", "CPF", "Cliente");

        pessoa.Desativar();

        pessoa.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Desativar_PessoaJaInativa_DeveLancarDomainException()
    {
        var pessoa = new Pessoa("João", "CPF", "Cliente");
        pessoa.Desativar();

        Action acao = () => pessoa.Desativar();

        acao.Should().Throw<DomainException>()
            .WithMessage("Pessoa já está inativa.");
    }
}