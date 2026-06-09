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

    public async Task<Produto> CriarAsync(string nome, string? descricao, string? codigo,
        string? codigoBarras, decimal precoVenda, decimal precoCusto,
        string? unidade, int? categoriaId, int estoqueMinimo)
    {
        var produto = new Produto
        {
            Nome = nome,
            Descricao = descricao,
            Codigo = codigo,
            CodigoBarras = codigoBarras,
            PrecoVenda = precoVenda,
            PrecoCusto = precoCusto,
            Unidade = unidade,
            CategoriaId = categoriaId,
            EstoqueMinimo = estoqueMinimo
        };

        await _produtoRepository.AdicionarAsync(produto);
        await _produtoRepository.SalvarAsync();
        return produto;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string? descricao, string? codigo,
        string? codigoBarras, decimal precoVenda, decimal precoCusto,
        string? unidade, int? categoriaId, int estoqueMinimo)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(id);
        if (produto == null) return false;

        produto.Nome = nome;
        produto.Descricao = descricao;
        produto.Codigo = codigo;
        produto.CodigoBarras = codigoBarras;
        produto.PrecoVenda = precoVenda;
        produto.PrecoCusto = precoCusto;
        produto.Unidade = unidade;
        produto.CategoriaId = categoriaId;
        produto.EstoqueMinimo = estoqueMinimo;

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
}