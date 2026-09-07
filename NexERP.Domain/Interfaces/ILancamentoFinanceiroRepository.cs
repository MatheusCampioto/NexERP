using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface ILancamentoFinanceiroRepository : IRepository<LancamentoFinanceiro>
{
    Task<IEnumerable<LancamentoFinanceiro>> ListarPorTipoAsync(string tipo);
}
