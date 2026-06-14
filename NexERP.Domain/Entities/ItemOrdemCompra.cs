namespace NexERP.Domain.Entities;

public class ItemOrdemCompra
{
    public int Id { get; set; }
    public int OrdemCompraId { get; set; }
    public OrdemCompra OrdemCompra { get; set; } = null!;
    public int? ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; } = 1;
    public decimal ValorUnitario { get; set; }
    public decimal Subtotal => Quantidade * ValorUnitario;
}