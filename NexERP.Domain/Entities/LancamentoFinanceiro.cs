namespace NexERP.Domain.Entities;

public class LancamentoFinanceiro
{
    public int Id { get; set; }
    public string Tipo { get; set; } = "Receita";
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime? DataPagamento { get; set; }
    public string Status { get; set; } = "Aberto";
    public string? Categoria { get; set; }
    public int? PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }
    public int? PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}