namespace NexERP.Domain.Entities;

public class OrdemCompra
{
    public int Id { get; set; }
    public int? SolicitacaoCompraId { get; set; }
    public SolicitacaoCompra? SolicitacaoCompra { get; set; }
    public int FornecedorId { get; set; }
    public Pessoa Fornecedor { get; set; } = null!;
    public int? CondicaoPagamentoId { get; set; }
    public CondicaoPagamento? CondicaoPagamento { get; set; }
    public string Status { get; set; } = "Aberta"; // Aberta, Enviada, Recebida, Cancelada
    public DateTime? DataPrevista { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemOrdemCompra> Itens { get; set; } = new();
}