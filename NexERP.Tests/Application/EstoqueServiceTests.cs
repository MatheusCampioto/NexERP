using FluentAssertions;
using Moq;
using NexERP.Application.Services;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Tests.Application;

public class EstoqueServiceTests
{
    private readonly Mock<IMovimentacaoEstoqueRepository> _movimentacaoMock;
    private readonly Mock<IProdutoRepository> _produtoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly EstoqueService _service;

    public EstoqueServiceTests()
    {
        _movimentacaoMock = new Mock<IMovimentacaoEstoqueRepository>();
        _produtoMock = new Mock<IProdutoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _service = new EstoqueService(
            _movimentacaoMock.Object,
            _produtoMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Movimentar_ProdutoInexistente_DeveRetornarFalse()
    {
        _produtoMock.Setup(r => r.BuscarPorIdAsync(99))
            .ReturnsAsync((Produto?)null);

        var resultado = await _service.MovimentarAsync(99, "Entrada", 10, null);

        resultado.sucesso.Should().BeFalse();
        resultado.mensagem.Should().Contain("nao encontrado");
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task Movimentar_SaidaComEstoqueInsuficiente_DeveRetornarFalse()
    {
        var produto = new Produto { Id = 1, Nome = "Produto A", EstoqueAtual = 5 };
        _produtoMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(produto);

        var resultado = await _service.MovimentarAsync(1, "Saida", 10, null);

        resultado.sucesso.Should().BeFalse();
        resultado.mensagem.Should().Contain("insuficiente");
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task Movimentar_Entrada_DeveAumentarEstoque()
    {
        var produto = new Produto { Id = 1, Nome = "Produto A", EstoqueAtual = 10 };
        _produtoMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(produto);

        var resultado = await _service.MovimentarAsync(1, "Entrada", 5, "Reposição");

        resultado.sucesso.Should().BeTrue();
        produto.EstoqueAtual.Should().Be(15);
        _movimentacaoMock.Verify(m => m.AdicionarAsync(It.IsAny<MovimentacaoEstoque>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Movimentar_Saida_DeveDiminuirEstoque()
    {
        var produto = new Produto { Id = 1, Nome = "Produto A", EstoqueAtual = 10 };
        _produtoMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(produto);

        var resultado = await _service.MovimentarAsync(1, "Saida", 3, null);

        resultado.sucesso.Should().BeTrue();
        produto.EstoqueAtual.Should().Be(7);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task AjustarInventario_QuantidadeIgual_DeveRetornarSemCommit()
    {
        var produto = new Produto { Id = 1, Nome = "Produto A", EstoqueAtual = 10 };
        _produtoMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(produto);

        var resultado = await _service.AjustarInventarioAsync(1, 10, null);

        resultado.sucesso.Should().BeTrue();
        resultado.mensagem.Should().Contain("correto");
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task AjustarInventario_QuantidadeDiferente_DeveAjustarECommit()
    {
        var produto = new Produto { Id = 1, Nome = "Produto A", EstoqueAtual = 10 };
        _produtoMock.Setup(r => r.BuscarPorIdAsync(1))
            .ReturnsAsync(produto);

        var resultado = await _service.AjustarInventarioAsync(1, 15, "Contagem física");

        resultado.sucesso.Should().BeTrue();
        produto.EstoqueAtual.Should().Be(15);
        _movimentacaoMock.Verify(m => m.AdicionarAsync(It.IsAny<MovimentacaoEstoque>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ListarEstoqueBaixo_DeveRetornarSoProdutosAbaixoDoMinimo()
    {
        var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "A", EstoqueAtual = 2, EstoqueMinimo = 5 },
            new Produto { Id = 2, Nome = "B", EstoqueAtual = 10, EstoqueMinimo = 5 },
            new Produto { Id = 3, Nome = "C", EstoqueAtual = 5, EstoqueMinimo = 5 }
        };
        _produtoMock.Setup(r => r.ListarTodosAsync())
            .ReturnsAsync(produtos);

        var resultado = await _service.ListarProdutosEstoqueBaixoAsync();

        resultado.Should().HaveCount(2);
        resultado.Should().Contain(p => p.Nome == "A");
        resultado.Should().Contain(p => p.Nome == "C");
    }
}