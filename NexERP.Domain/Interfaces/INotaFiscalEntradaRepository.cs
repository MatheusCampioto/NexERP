using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface INotaFiscalEntradaRepository
{
    Task<IEnumerable<NotaFiscalEntrada>> ListarTodosAsync();
    Task<NotaFiscalEntrada?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(NotaFiscalEntrada nf);
    Task AtualizarAsync(NotaFiscalEntrada nf);
    Task SalvarAsync();
}