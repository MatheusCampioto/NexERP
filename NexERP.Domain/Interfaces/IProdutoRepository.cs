using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> ListarTodosAsync();
    Task<Produto?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task<bool> ExisteAsync(int id);
    Task SalvarAsync();
}