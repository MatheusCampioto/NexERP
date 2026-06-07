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
        decimal precoVenda, decimal precoCusto, string? unidade, string? categoria, int estoqueMinimo)
    {
        var produto = new Produto
        {
            Nome = nome,
            Descricao = descricao,
            Codigo = codigo,
            PrecoVenda = precoVenda,
            PrecoCusto = precoCusto,
            Unidade = unidade,
            Categoria = categoria,
            EstoqueMinimo = estoqueMinimo
        };

        await _produtoRepository.AdicionarAsync(produto);
        await _produtoRepository.SalvarAsync();
        return produto;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string? descricao, string? codigo,
        decimal precoVenda, decimal precoCusto, string? unidade, string? categoria, int estoqueMinimo)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(id);
        if (produto == null) return false;

        produto.Nome = nome;
        produto.Descricao = descricao;
        produto.Codigo = codigo;
        produto.PrecoVenda = precoVenda;
        produto.PrecoCusto = precoCusto;
        produto.Unidade = unidade;
        produto.Categoria = categoria;
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