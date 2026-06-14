namespace NexERP.Domain.Entities;

public class NotaFiscalEntrada
{
    public int Id { get; set; }
    public int OrdemCompraId { get; set; }
    public OrdemCompra OrdemCompra { get; set; } = null!;
    public string NumeroNF { get; set; } = string.Empty;
    public string? Serie { get; set; }
    public string? ChaveAcesso { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataEntrada { get; set; } = DateTime.UtcNow;
    public decimal ValorProdutos { get; set; }
    public decimal ValorFrete { get; set; }
    public decimal ValorImpostos { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; set; }
    public bool EstoqueAtualizado { get; set; } = false;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemNotaFiscalEntrada> Itens { get; set; } = new();
}