using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface ICondicaoPagamentoRepository
{
    Task<IEnumerable<CondicaoPagamento>> ListarTodosAsync();
    Task<CondicaoPagamento?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(CondicaoPagamento condicao);
    Task AtualizarAsync(CondicaoPagamento condicao);
    Task SalvarAsync();
}