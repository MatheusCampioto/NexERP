namespace NexERP.Domain.Entities;

public class Produto
{
    public int Id { get; set; }

    // Identificação
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? Codigo { get; set; }
    public string? CodigoBarras { get; set; }
    public string? Unidade { get; set; }
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    // Precificação
    public decimal PrecoVenda { get; set; }
    public decimal PrecoCusto { get; set; }
    public decimal? PrecoMinimo { get; set; }
    public decimal? MargemLucro => PrecoCusto > 0
        ? Math.Round((PrecoVenda - PrecoCusto) / PrecoCusto * 100, 2)
        : null;

    // Fornecedor padrão
    public int? FornecedorId { get; set; }
    public Pessoa? Fornecedor { get; set; }

    // Fiscal
    public string? NCM { get; set; }
    public string? CEST { get; set; }
    public string? CFOP { get; set; }
    public string? OrigemMercadoria { get; set; } // 0-Nacional, 1-Estrangeira importação direta, etc
    public string? CSOSN { get; set; } // Simples Nacional
    public string? CST_ICMS { get; set; } // Lucro Presumido/Real
    public string? CST_PIS { get; set; }
    public string? CST_COFINS { get; set; }
    public decimal? AliquotaICMS { get; set; }
    public decimal? AliquotaIPI { get; set; }
    public decimal? AliquotaPIS { get; set; }
    public decimal? AliquotaCOFINS { get; set; }

    // Dimensões e peso
    public decimal? PesoBruto { get; set; }
    public decimal? PesoLiquido { get; set; }
    public decimal? Altura { get; set; }
    public decimal? Largura { get; set; }
    public decimal? Comprimento { get; set; }

    // Estoque
    public int EstoqueAtual { get; set; } = 0;
    public int EstoqueMinimo { get; set; } = 0;
    public int? EstoqueMaximo { get; set; }
    public string? LocalizacaoEstoque { get; set; } // Ex: A1-P2-C3
    public bool ControlaValidade { get; set; } = false;
    public int? DiasValidade { get; set; }

    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}