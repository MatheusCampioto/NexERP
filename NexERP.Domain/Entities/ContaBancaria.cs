namespace NexERP.Domain.Entities;

public class ContaBancaria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Banco { get; set; }
    public string? Agencia { get; set; }
    public string? NumeroConta { get; set; }
    public decimal SaldoInicial { get; set; } = 0;
    public decimal SaldoAtual { get; set; } = 0;
    public bool Ativa { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}