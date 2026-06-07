namespace NexERP.Domain.Entities;

public class MovimentacaoEstoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    public string Tipo { get; set; } = "Entrada"; // Entrada, Saida
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}