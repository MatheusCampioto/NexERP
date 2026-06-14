namespace NexERP.Domain.Entities;

public class SolicitacaoCompra
{
    public int Id { get; set; }
    public string Status { get; set; } = "Rascunho"; // Rascunho, Pendente, Aprovada, Reprovada, Cancelada
    public string? Observacao { get; set; }
    public string? MotivoReprovacao { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemSolicitacaoCompra> Itens { get; set; } = new();
}