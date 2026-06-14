using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IOrdemServicoRepository
{
    Task<IEnumerable<OrdemServico>> ListarTodosAsync();
    Task<OrdemServico?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(OrdemServico ordemServico);
    Task AtualizarAsync(OrdemServico ordemServico);
    Task SalvarAsync();
}