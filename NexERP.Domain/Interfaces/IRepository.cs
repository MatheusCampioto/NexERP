namespace NexERP.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> ListarTodosAsync();
    Task<T?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(T entity);
    Task AtualizarAsync(T entity);
    Task<bool> ExisteAsync(int id);
}