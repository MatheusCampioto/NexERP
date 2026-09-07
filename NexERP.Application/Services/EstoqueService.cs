using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class EstoqueService
{
    private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EstoqueService(IMovimentacaoEstoqueRepository movimentacaoRepository,
        IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ListarMovimentacoesPorProdutoAsync(int produtoId)
        => await _movimentacaoRepository.ListarPorProdutoAsync(produtoId);

    public async Task<IEnumerable<Produto>> ListarProdutosEstoqueBaixoAsync()
    {
        var produtos = await _produtoRepository.ListarTodosAsync();
        return produtos.Where(p => p.EstoqueAtual <= p.EstoqueMinimo);
    }

    public async Task<(bool sucesso, string mensagem)> MovimentarAsync(
        int produtoId, string tipo, int quantidade, string? observacao)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(produtoId);
        if (produto == null) return (false, "Produto nao encontrado.");

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
        await _unitOfWork.CommitAsync();

        return (true, "Movimentacao realizada com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> AjustarInventarioAsync(
        int produtoId, int quantidadeReal, string? observacao)
    {
        var produto = await _produtoRepository.BuscarPorIdAsync(produtoId);
        if (produto == null) return (false, "Produto nao encontrado.");

        var diferenca = quantidadeReal - produto.EstoqueAtual;
        if (diferenca == 0) return (true, "Estoque ja esta correto.");

        var tipo = diferenca > 0 ? "Entrada" : "Saida";
        var quantidade = Math.Abs(diferenca);

        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = produtoId,
            Tipo = tipo,
            Quantidade = quantidade,
            Observacao = observacao ?? $"Ajuste de inventario: de {produto.EstoqueAtual} para {quantidadeReal}"
        };

        produto.EstoqueAtual = quantidadeReal;

        await _movimentacaoRepository.AdicionarAsync(movimentacao);
        await _produtoRepository.AtualizarAsync(produto);
        await _unitOfWork.CommitAsync();

        return (true, $"Inventario ajustado. Diferenca: {(diferenca > 0 ? "+" : "")}{diferenca}");
    }
}
