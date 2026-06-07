using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IContaBancariaRepository
{
    Task<IEnumerable<ContaBancaria>> ListarTodosAsync();
    Task<ContaBancaria?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(ContaBancaria conta);
    Task AtualizarAsync(ContaBancaria conta);
    Task SalvarAsync();
}