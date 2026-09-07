using FluentAssertions;
using NexERP.Domain.Entities;
using NexERP.Domain.Enums;
using NexERP.Domain.Exceptions;

namespace NexERP.Tests.Domain;

public class PedidoTests
{
    [Fact]
    public void Criar_PedidoComPessoaIdValido_DeveIniciarComoOrcamento()
    {
        var pedido = new Pedido(1);

        pedido.Status.Should().Be(StatusPedido.Orcamento);
        pedido.Itens.Should().BeEmpty();
        pedido.ValorTotal.Should().Be(0);
    }

    [Fact]
    public void Criar_PedidoComPessoaIdInvalido_DeveLancarDomainException()
    {
        Action acao = () => new Pedido(0);

        acao.Should().Throw<DomainException>()
            .WithMessage("PessoaId inválido.");
    }

    [Fact]
    public void Confirmar_PedidoSemItens_DeveLancarDomainException()
    {
        var pedido = new Pedido(1);

        Action acao = () => pedido.Confirmar();

        acao.Should().Throw<DomainException>()
            .WithMessage("Pedido sem itens não pode ser confirmado.");
    }

    [Fact]
    public void Confirmar_PedidoCancelado_DeveLancarDomainException()
    {
        var pedido = new Pedido(1);
        pedido.Itens.Add(new ItemPedido { ProdutoId = 1, Quantidade = 1, PrecoUnitario = 100 });
        pedido.Cancelar();

        Action acao = () => pedido.Confirmar();

        acao.Should().Throw<DomainException>()
            .WithMessage("Pedido cancelado não pode ser confirmado.");
    }

    [Fact]
    public void Confirmar_PedidoComItens_DeveAlterarStatusParaConfirmado()
    {
        var pedido = new Pedido(1);
        pedido.Itens.Add(new ItemPedido { ProdutoId = 1, Quantidade = 2, PrecoUnitario = 50 });

        pedido.Confirmar();

        pedido.Status.Should().Be(StatusPedido.Confirmado);
    }

    [Fact]
    public void Cancelar_PedidoFaturado_DeveLancarDomainException()
    {
        var pedido = new Pedido(1);
        pedido.Status = StatusPedido.Faturado;

        Action acao = () => pedido.Cancelar();

        acao.Should().Throw<DomainException>()
            .WithMessage("Pedido já faturado não pode ser cancelado.");
    }

    [Fact]
    public void RecalcularTotal_DeveCalcularSomaCorreta()
    {
        var pedido = new Pedido(1);
        pedido.Itens.Add(new ItemPedido { ProdutoId = 1, Quantidade = 2, PrecoUnitario = 100 });
        pedido.Itens.Add(new ItemPedido { ProdutoId = 2, Quantidade = 3, PrecoUnitario = 50 });

        pedido.RecalcularTotal();

        pedido.ValorTotal.Should().Be(350);
    }
}