using NexERP.Domain.Entities;
using NexERP.Application.Interfaces;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class OrdemCompraService : IOrdemCompraService
{
    private readonly IOrdemCompraRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public OrdemCompraService(IOrdemCompraRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrdemCompra>> ListarTodosAsync()
        => await _repository.ListarTodosAsync();

    public async Task<OrdemCompra?> BuscarPorIdAsync(int id)
        => await _repository.BuscarPorIdAsync(id);

    public async Task<OrdemCompra> CriarAsync(int fornecedorId, int? solicitacaoCompraId,
        int? condicaoPagamentoId, DateTime? dataPrevista, string? observacao,
        List<(int? produtoId, string descricao, decimal quantidade, decimal valorUnitario)> itens)
    {
        var ordem = new OrdemCompra
        {
            FornecedorId = fornecedorId,
            SolicitacaoCompraId = solicitacaoCompraId,
            CondicaoPagamentoId = condicaoPagamentoId,
            DataPrevista = dataPrevista.HasValue
                ? DateTime.SpecifyKind(dataPrevista.Value, DateTimeKind.Utc)
                : null,
            Observacao = observacao,
            Status = "Aberta"
        };

        foreach (var (produtoId, descricao, quantidade, valorUnitario) in itens)
        {
            ordem.Itens.Add(new ItemOrdemCompra
            {
                ProdutoId = produtoId,
                Descricao = descricao,
                Quantidade = quantidade,
                ValorUnitario = valorUnitario
            });
        }

        ordem.ValorTotal = ordem.Itens.Sum(i => i.Quantidade * i.ValorUnitario);

        await _repository.AdicionarAsync(ordem);
        await _unitOfWork.CommitAsync();
        return ordem;
    }

    public async Task<(bool sucesso, string mensagem)> AtualizarStatusAsync(int id, string novoStatus)
    {
        var ordem = await _repository.BuscarPorIdAsync(id);
        if (ordem == null) return (false, "Ordem nao encontrada.");
        ordem.Status = novoStatus;
        await _repository.AtualizarAsync(ordem);
        await _unitOfWork.CommitAsync();
        return (true, $"Status atualizado para {novoStatus}.");
    }

    public async Task<(bool sucesso, string mensagem)> CancelarAsync(int id)
    {
        var ordem = await _repository.BuscarPorIdAsync(id);
        if (ordem == null) return (false, "Ordem nao encontrada.");
        if (ordem.Status == "Recebida") return (false, "Ordem ja recebida nao pode ser cancelada.");
        ordem.Status = "Cancelada";
        await _repository.AtualizarAsync(ordem);
        await _unitOfWork.CommitAsync();
        return (true, "Ordem cancelada.");
    }
}

