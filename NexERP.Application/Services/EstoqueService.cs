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
}