using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class OrdemCompraService
{
    private readonly IOrdemCompraRepository _repository;

    public OrdemCompraService(IOrdemCompraRepository repository)
    {
        _repository = repository;
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
        await _repository.SalvarAsync();
        return ordem;
    }

    public async Task<(bool sucesso, string mensagem)> AtualizarStatusAsync(int id, string novoStatus)
    {
        var ordem = await _repository.BuscarPorIdAsync(id);
        if (ordem == null) return (false, "Ordem não encontrada.");
        ordem.Status = novoStatus;
        await _repository.AtualizarAsync(ordem);
        await _repository.SalvarAsync();
        return (true, $"Status atualizado para {novoStatus}.");
    }

    public async Task<(bool sucesso, string mensagem)> CancelarAsync(int id)
    {
        var ordem = await _repository.BuscarPorIdAsync(id);
        if (ordem == null) return (false, "Ordem não encontrada.");
        if (ordem.Status == "Recebida") return (false, "Ordem já recebida não pode ser cancelada.");
        ordem.Status = "Cancelada";
        await _repository.AtualizarAsync(ordem);
        await _repository.SalvarAsync();
        return (true, "Ordem cancelada.");
    }
}