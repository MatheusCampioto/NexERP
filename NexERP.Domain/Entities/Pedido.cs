namespace NexERP.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
    public string Status { get; set; } = "Orcamento"; // Orcamento, Pedido, Confirmado, Cancelado
    public decimal ValorTotal { get; set; }
    public decimal Desconto { get; set; } = 0;
    public decimal ValorLiquido => ValorTotal - Desconto;
    public string? CondicaoPagamento { get; set; } // Vista, 30 dias, 30/60, etc
    public string? FormaPagamento { get; set; } // Dinheiro, Cartao, Boleto, Pix
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemPedido> Itens { get; set; } = new();
}