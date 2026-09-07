using NexERP.Domain.Exceptions;

namespace NexERP.Domain.Entities;

public class Pessoa
{
    // EF Core precisa de construtor sem parâmetros
    protected Pessoa() { }

    public Pessoa(string nome, string tipoDocumento, string tipo)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome é obrigatório.");

        if (tipoDocumento != "CPF" && tipoDocumento != "CNPJ")
            throw new DomainException("TipoDocumento deve ser CPF ou CNPJ.");

        var tiposValidos = new[] { "Cliente", "Fornecedor", "Representante", "Funcionario", "Transportadora" };
        if (!tiposValidos.Contains(tipo))
            throw new DomainException($"Tipo inválido: {tipo}.");

        Nome = nome;
        TipoDocumento = tipoDocumento;
        Tipo = tipo;
        Ativo = true;
        CriadoEm = DateTime.UtcNow;
    }

    public int Id { get; set; }

    // Identificação
    public string TipoDocumento { get; set; } = "CPF";
    public string Tipo { get; set; } = "Cliente";
    public string? Funcao { get; set; }

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

    // Comportamento
    public void Validar()
    {
        if (TipoDocumento == "CPF" && string.IsNullOrWhiteSpace(CPF))
            throw new DomainException("CPF é obrigatório para Pessoa Física.");

        if (TipoDocumento == "CNPJ" && string.IsNullOrWhiteSpace(CNPJ))
            throw new DomainException("CNPJ é obrigatório para Pessoa Jurídica.");

        if (TipoDocumento == "CNPJ" && string.IsNullOrWhiteSpace(RazaoSocial))
            throw new DomainException("Razão Social é obrigatória para Pessoa Jurídica.");
    }

    public void Desativar()
    {
        if (!Ativo)
            throw new DomainException("Pessoa já está inativa.");
        Ativo = false;
    }
}