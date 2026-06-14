namespace NexERP.Domain.Entities;

public class ItemSolicitacaoCompra
{
    public int Id { get; set; }
    public int SolicitacaoCompraId { get; set; }
    public SolicitacaoCompra SolicitacaoCompra { get; set; } = null!;
    public int? ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; } = 1;
    public string? Unidade { get; set; }
    public string? Observacao { get; set; }
}