using NexERP.Domain.Entities;
using NexERP.Application.Interfaces;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class NotaFiscalEntradaService : INotaFiscalEntradaService
{
    private readonly INotaFiscalEntradaRepository _nfRepository;
    private readonly IOrdemCompraRepository _ordemRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMovimentacaoEstoqueRepository _estoqueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotaFiscalEntradaService(
        INotaFiscalEntradaRepository nfRepository,
        IOrdemCompraRepository ordemRepository,
        IProdutoRepository produtoRepository,
        IMovimentacaoEstoqueRepository estoqueRepository,
        IUnitOfWork unitOfWork)
    {
        _nfRepository = nfRepository;
        _ordemRepository = ordemRepository;
        _produtoRepository = produtoRepository;
        _estoqueRepository = estoqueRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<NotaFiscalEntrada>> ListarTodosAsync()
        => await _nfRepository.ListarTodosAsync();

    public async Task<NotaFiscalEntrada?> BuscarPorIdAsync(int id)
        => await _nfRepository.BuscarPorIdAsync(id);

    public async Task<NotaFiscalEntrada> CriarAsync(int ordemCompraId, string numeroNF,
        string? serie, string? chaveAcesso, DateTime dataEmissao,
        decimal valorProdutos, decimal valorFrete, decimal valorImpostos, string? observacao,
        List<(int? produtoId, string descricao, decimal quantidade, decimal valorUnitario)> itens)
    {
        var nf = new NotaFiscalEntrada
        {
            OrdemCompraId = ordemCompraId,
            NumeroNF = numeroNF,
            Serie = serie,
            ChaveAcesso = chaveAcesso,
            DataEmissao = DateTime.SpecifyKind(dataEmissao, DateTimeKind.Utc),
            DataEntrada = DateTime.UtcNow,
            ValorProdutos = valorProdutos,
            ValorFrete = valorFrete,
            ValorImpostos = valorImpostos,
            ValorTotal = valorProdutos + valorFrete + valorImpostos,
            Observacao = observacao
        };

        foreach (var (produtoId, descricao, quantidade, valorUnitario) in itens)
        {
            nf.Itens.Add(new ItemNotaFiscalEntrada
            {
                ProdutoId = produtoId,
                Descricao = descricao,
                Quantidade = quantidade,
                ValorUnitario = valorUnitario
            });
        }

        await _nfRepository.AdicionarAsync(nf);
        await _unitOfWork.CommitAsync();
        return nf;
    }

    public async Task<(bool sucesso, string mensagem)> DarEntradaEstoqueAsync(int nfId)
    {
        var nf = await _nfRepository.BuscarPorIdAsync(nfId);
        if (nf == null) return (false, "NF nao encontrada.");
        if (nf.EstoqueAtualizado) return (false, "Estoque ja foi atualizado para esta NF.");

        foreach (var item in nf.Itens.Where(i => i.ProdutoId.HasValue))
        {
            var produto = await _produtoRepository.BuscarPorIdAsync(item.ProdutoId!.Value);
            if (produto == null) continue;

            produto.EstoqueAtual += (int)item.Quantidade;
            await _produtoRepository.AtualizarAsync(produto);

            await _estoqueRepository.AdicionarAsync(new MovimentacaoEstoque
            {
                ProdutoId = item.ProdutoId!.Value,
                Tipo = "Entrada",
                Quantidade = (int)item.Quantidade,
                Observacao = $"NF de Entrada #{nf.NumeroNF}"
            });
        }

        nf.EstoqueAtualizado = true;
        var ordem = await _ordemRepository.BuscarPorIdAsync(nf.OrdemCompraId);
        if (ordem != null)
        {
            ordem.Status = "Recebida";
            await _ordemRepository.AtualizarAsync(ordem);
        }

        await _nfRepository.AtualizarAsync(nf);
        await _unitOfWork.CommitAsync();
        return (true, "Estoque atualizado com sucesso.");
    }
}

