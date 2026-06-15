namespace NexERP.Domain.Entities;

public class ConfiguracaoSistema
{
    public int Id { get; set; }

    // Dados da Empresa
    public string NomeEmpresa { get; set; } = string.Empty;
    public string? NomeFantasia { get; set; }
    public string? CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string? InscricaoMunicipal { get; set; }
    public string? RegimeTributario { get; set; } // SimplesNacional, LucroPresumido, LucroReal

    // Contato
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Site { get; set; }

    // Endereço
    public string? CEP { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    // Fiscal
    public string? CFOP_PadraoVenda { get; set; }
    public string? CFOP_PadraoCompra { get; set; }
    public decimal? AliquotaICMS_Padrao { get; set; }
    public decimal? AliquotaPIS_Padrao { get; set; }
    public decimal? AliquotaCOFINS_Padrao { get; set; }

    // Sistema
    public string? MoedaSimbolo { get; set; } = "R$";
    public string? LogoUrl { get; set; }

    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}