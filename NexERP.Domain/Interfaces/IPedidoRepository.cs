using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> ListarTodosAsync();
    Task<Pedido?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Pedido pedido);
    Task AtualizarAsync(Pedido pedido);
    Task SalvarAsync();
}