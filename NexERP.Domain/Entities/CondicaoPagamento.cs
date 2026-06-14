namespace NexERP.Domain.Entities;

public class CondicaoPagamento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Ex: 30/60/90, À Vista
    public string? Descricao { get; set; }
    public int NumeroParcelas { get; set; } = 1;
    public int DiasEntreParcelas { get; set; } = 30;
    public int PrimeiroPagamentoDias { get; set; } = 0; // dias após emissão
    public bool Ativa { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}