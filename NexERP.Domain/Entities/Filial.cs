namespace NexERP.Domain.Entities;

public class Filial
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string? NomeFantasia { get; set; }
    public string? CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? InscricaoMunicipal { get; set; }
    public string? CNAE { get; set; }
    public string? CRT { get; set; }
    public string? Modelo { get; set; }
    public string? SerieDanfe { get; set; }
    public string? SimplesNacional { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? CEP { get; set; }
    public string? Endereco { get; set; }
    public string? NumeroEnd { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public int? PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }
    public bool Ativa { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}