using NexERP.Domain.Entities;
using NexERP.Domain.Enums;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class PedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;

    public PedidoService(IPedidoRepository pedidoRepository,
        IProdutoRepository produtoRepository,
        IMovimentacaoEstoqueRepository movimentacaoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<IEnumerable<Pedido>> ListarTodosAsync()
        => await _pedidoRepository.ListarTodosAsync();

    public async Task<Pedido?> BuscarPorIdAsync(int id)
        => await _pedidoRepository.BuscarPorIdAsync(id);

    public async Task<(bool sucesso, string mensagem, Pedido? pedido)> CriarAsync(
        int pessoaId, string? observacao, int? condicaoPagamentoId,
        FormaPagamento? formaPagamento, decimal desconto,
        List<(int produtoId, int quantidade, decimal desconto)> itens)
    {
        var pedido = new Pedido(pessoaId)
        {
            Observacao = observacao,
            CondicaoPagamentoId = condicaoPagamentoId,
            FormaPagamento = formaPagamento,
            Desconto = desconto
        };

        foreach (var (produtoId, quantidade, descontoItem) in itens)
        {
            var produto = await _produtoRepository.BuscarPorIdAsync(produtoId);
            if (produto == null)
                return (false, $"Produto {produtoId} não encontrado.", null);

            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = produtoId,
                Quantidade = quantidade,
                PrecoUnitario = produto.PrecoVenda,
                Desconto = descontoItem
            });
        }

        pedido.RecalcularTotal();

        await _pedidoRepository.AdicionarAsync(pedido);
        await _pedidoRepository.SalvarAsync();

        return (true, "Pedido criado com sucesso.", pedido);
    }

    public async Task<(bool sucesso, string mensagem)> AtualizarAsync(
        int id, int pessoaId, string? observacao, int? condicaoPagamentoId,
        FormaPagamento? formaPagamento, decimal desconto,
        List<(int produtoId, int quantidade, decimal desconto)> itens)
    {
        var pedido = await _pedidoRepository.BuscarPorIdAsync(id);
        if (pedido == null) return (false, "Pedido não encontrado.");

        if (pedido.Status == StatusPedido.Confirmado || pedido.Status == StatusPedido.Cancelado)
            return (false, "Pedido não pode ser editado neste status.");

        pedido.PessoaId = pessoaId;
        pedido.Observacao = observacao;
        pedido.CondicaoPagamentoId = condicaoPagamentoId;
        pedido.FormaPagamento = formaPagamento;
        pedido.Desconto = desconto;

        pedido.Itens.Clear();

        foreach (var (produtoId, quantidade, descontoItem) in itens)
        {
            var produto = await _produtoRepository.BuscarPorIdAsync(produtoId);
            if (produto == null) return (false, $"Produto {produtoId} não encontrado.");

            pedido.Itens.Add(new ItemPedido
            {
                ProdutoId = produtoId,
                Quantidade = quantidade,
                PrecoUnitario = produto.PrecoVenda,
                Desconto = descontoItem
            });
        }

        pedido.RecalcularTotal();

        await _pedidoRepository.AtualizarAsync(pedido);
        await _pedidoRepository.SalvarAsync();

        return (true, "Pedido atualizado com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> AvancarStatusAsync(int id)
    {
        var pedido = await _pedidoRepository.BuscarPorIdAsync(id);
        if (pedido == null) return (false, "Pedido não encontrado.");

        if (pedido.Status == StatusPedido.Orcamento)
        {
            pedido.Status = StatusPedido.Pedido;
            await _pedidoRepository.AtualizarAsync(pedido);
            await _pedidoRepository.SalvarAsync();
            return (true, "Orçamento convertido em Pedido.");
        }

        if (pedido.Status == StatusPedido.Pedido)
        {
            foreach (var item in pedido.Itens)
            {
                var produto = await _produtoRepository.BuscarPorIdAsync(item.ProdutoId);
                if (produto == null) return (false, $"Produto {item.ProdutoId} não encontrado.");
                if (produto.EstoqueAtual < item.Quantidade)
                    return (false, $"Estoque insuficiente para {produto.Nome}. Estoque atual: {produto.EstoqueAtual}");

                produto.EstoqueAtual -= item.Quantidade;
                await _produtoRepository.AtualizarAsync(produto);

                await _movimentacaoRepository.AdicionarAsync(new MovimentacaoEstoque
                {
                    ProdutoId = item.ProdutoId,
                    Tipo = "Saida",
                    Quantidade = item.Quantidade,
                    Observacao = $"Pedido #{id}"
                });
            }

            pedido.Confirmar();
            await _pedidoRepository.AtualizarAsync(pedido);
            await _pedidoRepository.SalvarAsync();
            return (true, "Pedido confirmado e estoque atualizado.");
        }

        return (false, "Pedido já está confirmado ou cancelado.");
    }

    public async Task<(bool sucesso, string mensagem)> CancelarAsync(int id)
    {
        var pedido = await _pedidoRepository.BuscarPorIdAsync(id);
        if (pedido == null) return (false, "Pedido não encontrado.");

        pedido.Cancelar();
        await _pedidoRepository.AtualizarAsync(pedido);
        await _pedidoRepository.SalvarAsync();

        return (true, "Pedido cancelado.");
    }
}