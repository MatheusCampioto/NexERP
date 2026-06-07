namespace NexERP.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
    public string Status { get; set; } = "Aberto"; // Aberto, Confirmado, Cancelado
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemPedido> Itens { get; set; } = new();
}