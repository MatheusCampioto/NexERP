using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IFilialRepository
{
    Task<IEnumerable<Filial>> ListarTodosAsync();
    Task<Filial?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Filial filial);
    Task AtualizarAsync(Filial filial);
    Task SalvarAsync();
}