using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<Produto>> ListarTodosAsync()
        => await _produtoRepository.ListarTodosAsync();

    public async Task<Produto?> BuscarPorIdAsync(int id)
        => await _produtoRepository.BuscarPorIdAsync(id);

    public async Task<Produto> CriarAsync(ProdutoDto dto)
    {
        var produto = MapearDto(new Produto(), dto);
        await _produtoRepository.AdicionarAsync(produto);
        await _produtoRepository.SalvarAsync();
        return produto;
    }

    public async Task<bool> AtualizarAsync(int id, ProdutoDto dto)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(id);
        if (produto == null) return false;
        MapearDto(produto, dto);
        await _produtoRepository.AtualizarAsync(produto);
        await _produtoRepository.SalvarAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(id);
        if (produto == null) return false;
        produto.Ativo = false;
        await _produtoRepository.AtualizarAsync(produto);
        await _produtoRepository.SalvarAsync();
        return true;
    }

    private static Produto MapearDto(Produto produto, ProdutoDto dto)
    {
        produto.Nome = dto.Nome;
        produto.Descricao = dto.Descricao;
        produto.Codigo = dto.Codigo;
        produto.CodigoBarras = dto.CodigoBarras;
        produto.PrecoVenda = dto.PrecoVenda;
        produto.PrecoCusto = dto.PrecoCusto;
        produto.PrecoMinimo = dto.PrecoMinimo;
        produto.Unidade = dto.Unidade;
        produto.CategoriaId = dto.CategoriaId;
        produto.FornecedorId = dto.FornecedorId;
        produto.EstoqueMinimo = dto.EstoqueMinimo;
        produto.EstoqueMaximo = dto.EstoqueMaximo;
        produto.LocalizacaoEstoque = dto.LocalizacaoEstoque;
        produto.ControlaValidade = dto.ControlaValidade;
        produto.DiasValidade = dto.DiasValidade;
        produto.NCM = dto.NCM;
        produto.CEST = dto.CEST;
        produto.CFOP = dto.CFOP;
        produto.OrigemMercadoria = dto.OrigemMercadoria;
        produto.CSOSN = dto.CSOSN;
        produto.CST_ICMS = dto.CST_ICMS;
        produto.CST_PIS = dto.CST_PIS;
        produto.CST_COFINS = dto.CST_COFINS;
        produto.AliquotaICMS = dto.AliquotaICMS;
        produto.AliquotaIPI = dto.AliquotaIPI;
        produto.AliquotaPIS = dto.AliquotaPIS;
        produto.AliquotaCOFINS = dto.AliquotaCOFINS;
        produto.PesoBruto = dto.PesoBruto;
        produto.PesoLiquido = dto.PesoLiquido;
        produto.Altura = dto.Altura;
        produto.Largura = dto.Largura;
        produto.Comprimento = dto.Comprimento;
        return produto;
    }
}

public record ProdutoDto(
    string Nome,
    string? Descricao,
    string? Codigo,
    string? CodigoBarras,
    decimal PrecoVenda,
    decimal PrecoCusto,
    decimal? PrecoMinimo,
    string? Unidade,
    int? CategoriaId,
    int? FornecedorId,
    int EstoqueMinimo,
    int? EstoqueMaximo,
    string? LocalizacaoEstoque,
    bool ControlaValidade,
    int? DiasValidade,
    string? NCM,
    string? CEST,
    string? CFOP,
    string? OrigemMercadoria,
    string? CSOSN,
    string? CST_ICMS,
    string? CST_PIS,
    string? CST_COFINS,
    decimal? AliquotaICMS,
    decimal? AliquotaIPI,
    decimal? AliquotaPIS,
    decimal? AliquotaCOFINS,
    decimal? PesoBruto,
    decimal? PesoLiquido,
    decimal? Altura,
    decimal? Largura,
    decimal? Comprimento
);