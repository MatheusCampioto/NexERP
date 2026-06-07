using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ListarTodosAsync();
    Task<Pessoa?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Pessoa pessoa);
    Task AtualizarAsync(Pessoa pessoa);
    Task<bool> ExisteAsync(int id);
    Task SalvarAsync();
}