using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface ILancamentoFinanceiroRepository
{
    Task<IEnumerable<LancamentoFinanceiro>> ListarTodosAsync();
    Task<IEnumerable<LancamentoFinanceiro>> ListarPorTipoAsync(string tipo);
    Task<LancamentoFinanceiro?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(LancamentoFinanceiro lancamento);
    Task AtualizarAsync(LancamentoFinanceiro lancamento);
    Task SalvarAsync();
}