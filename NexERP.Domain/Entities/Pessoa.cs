namespace NexERP.Domain.Entities;

public class Pessoa
{
    public int Id { get; set; }

    // Identificação
    public string TipoDocumento { get; set; } = "CPF"; // CPF, CNPJ
    public string Tipo { get; set; } = "Cliente"; // Cliente, Fornecedor, Representante
    public string? Funcao { get; set; } // Ex: Representante Comercial, Distribuidor

    // Dados PF
    public string Nome { get; set; } = string.Empty;
    public string? CPF { get; set; }
    public string? RG { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? EstadoCivil { get; set; }
    public string? Profissao { get; set; }

    // Dados PJ
    public string? RazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }
    public string? CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? InscricaoMunicipal { get; set; }
    public string? NomeContato { get; set; }
    public string? Site { get; set; }

    // Contato
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }

    // Endereço
    public string? CEP { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    // Geral
    public string? Observacao { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}