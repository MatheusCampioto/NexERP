using NexERP.Domain.Enums;
using NexERP.Domain.Exceptions;

namespace NexERP.Domain.Entities;

public class Pedido
{
    // EF Core precisa de construtor sem parâmetros
    protected Pedido() { }

    public Pedido(int pessoaId)
    {
        if (pessoaId <= 0)
            throw new DomainException("PessoaId inválido.");

        PessoaId = pessoaId;
        Status = StatusPedido.Orcamento;
        Desconto = 0;
        CriadoEm = DateTime.UtcNow;
        Itens = new List<ItemPedido>();
    }

    public int Id { get; set; }
    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
    public StatusPedido Status { get; set; } = StatusPedido.Orcamento;
    public decimal ValorTotal { get; set; }
    public decimal Desconto { get; set; } = 0;
    public decimal ValorLiquido => ValorTotal - Desconto;
    public int? CondicaoPagamentoId { get; set; }
    public CondicaoPagamento? CondicaoPagamento { get; set; }
    public FormaPagamento? FormaPagamento { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemPedido> Itens { get; set; } = new();

    // Comportamento no domínio
    public void Confirmar()
    {
        if (!Itens.Any())
            throw new DomainException("Pedido sem itens não pode ser confirmado.");

        if (Status == StatusPedido.Cancelado)
            throw new DomainException("Pedido cancelado não pode ser confirmado.");

        Status = StatusPedido.Confirmado;
    }

    public void Cancelar()
    {
        if (Status == StatusPedido.Faturado)
            throw new DomainException("Pedido já faturado não pode ser cancelado.");

        Status = StatusPedido.Cancelado;
    }

    public void RecalcularTotal()
    {
        ValorTotal = Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
    }
}