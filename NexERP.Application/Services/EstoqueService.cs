using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class EstoqueService
{
    private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public EstoqueService(IMovimentacaoEstoqueRepository movimentacaoRepository,
        IProdutoRepository produtoRepository)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ListarMovimentacoesPorProdutoAsync(int produtoId)
        => await _movimentacaoRepository.ListarPorProdutoAsync(produtoId);

    public async Task<IEnumerable<Produto>> ListarProdutosEstoqueBaixoAsync()
    {
        var produtos = await _produtoRepository.ListarTodosAsync();
        return produtos.Where(p => p.EstoqueAtual <= p.EstoqueMinimo);
    }

    public async Task<(bool sucesso, string mensagem)> MovimentarAsync(int produtoId, string tipo, int quantidade, string? observacao)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(produtoId);
        if (produto == null)
            return (false, "Produto não encontrado.");

        if (tipo == "Saida" && produto.EstoqueAtual < quantidade)
            return (false, $"Estoque insuficiente. Estoque atual: {produto.EstoqueAtual}");

        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = produtoId,
            Tipo = tipo,
            Quantidade = quantidade,
            Observacao = observacao
        };

        if (tipo == "Entrada")
            produto.EstoqueAtual += quantidade;
        else
            produto.EstoqueAtual -= quantidade;

        await _movimentacaoRepository.AdicionarAsync(movimentacao);
        await _produtoRepository.AtualizarAsync(produto);
        await _movimentacaoRepository.SalvarAsync();

        return (true, "Movimentação realizada com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> AjustarInventarioAsync(int produtoId, int quantidadeReal, string? observacao)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(produtoId);
        if (produto == null)
            return (false, "Produto não encontrado.");

        var diferenca = quantidadeReal - produto.EstoqueAtual;
        if (diferenca == 0)
            return (true, "Estoque já está correto.");

        var tipo = diferenca > 0 ? "Entrada" : "Saida";
        var quantidade = Math.Abs(diferenca);

        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = produtoId,
            Tipo = tipo,
            Quantidade = quantidade,
            Observacao = observacao ?? $"Ajuste de inventário — de {produto.EstoqueAtual} para {quantidadeReal}"
        };

        produto.EstoqueAtual = quantidadeReal;

        await _movimentacaoRepository.AdicionarAsync(movimentacao);
        await _produtoRepository.AtualizarAsync(produto);
        await _movimentacaoRepository.SalvarAsync();

        return (true, $"Inventário ajustado. Diferença: {(diferenca > 0 ? "+" : "")}{diferenca}");
    }
}