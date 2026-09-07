using FluentAssertions;
using Moq;
using NexERP.Application.Services;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Tests.Application;

public class PessoaServiceTests
{
    private readonly Mock<IPessoaRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly PessoaService _service;

    public PessoaServiceTests()
    {
        _repositoryMock = new Mock<IPessoaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _service = new PessoaService(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CriarAsync_DeveChamarAdicionarECommit()
    {
        var dto = CriarDto();

        var resultado = await _service.CriarAsync(dto);

        _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Pessoa>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        resultado.Should().NotBeNull();
        resultado.Nome.Should().Be("João Silva");
    }

    [Fact]
    public async Task AtualizarAsync_PessoaInexistente_DeveRetornarFalse()
    {
        _repositoryMock.Setup(r => r.BuscarPorIdAsync(99))
            .ReturnsAsync((Pessoa?)null);

        var resultado = await _service.AtualizarAsync(99, CriarDto());

        resultado.Should().BeFalse();
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task AtualizarAsync_PessoaExistente_DeveRetornarTrue()
    {
        var pessoa = new Pessoa { Nome = "Antigo" };
        _repositoryMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(pessoa);

        var resultado = await _service.AtualizarAsync(1, CriarDto());

        resultado.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task DesativarAsync_PessoaInexistente_DeveRetornarFalse()
    {
        _repositoryMock.Setup(r => r.BuscarPorIdAsync(99))
            .ReturnsAsync((Pessoa?)null);

        var resultado = await _service.DesativarAsync(99);

        resultado.Should().BeFalse();
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task DesativarAsync_PessoaExistente_DeveDesativarECommit()
    {
        var pessoa = new Pessoa { Nome = "João", Ativo = true };
        _repositoryMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(pessoa);

        var resultado = await _service.DesativarAsync(1);

        resultado.Should().BeTrue();
        pessoa.Ativo.Should().BeFalse();
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    private static PessoaDto CriarDto() => new(
        TipoDocumento: "CPF",
        Tipo: "Cliente",
        Funcao: null,
        Nome: "João Silva",
        CPF: "123.456.789-00",
        RG: null,
        DataNascimento: null,
        EstadoCivil: null,
        Profissao: null,
        RazaoSocial: null,
        NomeFantasia: null,
        CNPJ: null,
        InscricaoEstadual: null,
        InscricaoMunicipal: null,
        NomeContato: null,
        Site: null,
        Email: "joao@email.com",
        Telefone: null,
        Celular: null,
        CEP: null,
        Endereco: null,
        Numero: null,
        Complemento: null,
        Bairro: null,
        Cidade: null,
        Estado: null,
        Observacao: null
    );
}