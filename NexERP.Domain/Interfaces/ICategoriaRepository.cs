using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> ListarTodosAsync();
    Task<Categoria?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Categoria categoria);
    Task AtualizarAsync(Categoria categoria);
    Task SalvarAsync();
}