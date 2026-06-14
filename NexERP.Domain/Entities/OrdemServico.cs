namespace NexERP.Domain.Entities;

public class OrdemServico
{
    public int Id { get; set; }
    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; } = null!;
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Status { get; set; } = "Aberta"; // Aberta, EmAndamento, Concluida, Cancelada
    public string? Prioridade { get; set; } = "Normal"; // Baixa, Normal, Alta, Urgente
    public decimal? ValorEstimado { get; set; }
    public decimal? ValorFinal { get; set; }
    public DateTime? DataPrevista { get; set; }
    public DateTime? DataConclusao { get; set; }
    public string? Tecnico { get; set; }
    public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public List<ItemOrdemServico> Itens { get; set; } = new();
}