using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface ISolicitacaoCompraRepository
{
    Task<IEnumerable<SolicitacaoCompra>> ListarTodosAsync();
    Task<SolicitacaoCompra?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(SolicitacaoCompra solicitacao);
    Task AtualizarAsync(SolicitacaoCompra solicitacao);
    Task SalvarAsync();
}