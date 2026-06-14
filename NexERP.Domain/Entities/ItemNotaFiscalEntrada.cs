namespace NexERP.Domain.Entities;

public class ItemNotaFiscalEntrada
{
    public int Id { get; set; }
    public int NotaFiscalEntradaId { get; set; }
    public NotaFiscalEntrada NotaFiscalEntrada { get; set; } = null!;
    public int? ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Quantidade { get; set; } = 1;
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal => Quantidade * ValorUnitario;
}