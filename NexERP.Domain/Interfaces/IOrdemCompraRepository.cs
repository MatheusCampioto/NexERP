using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IOrdemCompraRepository
{
    Task<IEnumerable<OrdemCompra>> ListarTodosAsync();
    Task<OrdemCompra?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(OrdemCompra ordemCompra);
    Task AtualizarAsync(OrdemCompra ordemCompra);
    Task SalvarAsync();
}