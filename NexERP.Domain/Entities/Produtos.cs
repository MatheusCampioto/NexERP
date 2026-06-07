namespace NexERP.Domain.Entities;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Codigo { get; set; }
    public decimal PrecoVenda { get; set; }
    public decimal PrecoCusto { get; set; }
    public string? Unidade { get; set; } // UN, KG, L, CX
    public string? Categoria { get; set; }
    public int EstoqueAtual { get; set; } = 0;
    public int EstoqueMinimo { get; set; } = 0;
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}