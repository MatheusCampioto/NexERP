using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IOrdemServicoService
{
    Task<IEnumerable<OrdemServico>> ListarTodosAsync();
    Task<OrdemServico?> BuscarPorIdAsync(int id);
    Task<OrdemServico> CriarAsync(int pessoaId, string titulo, string? descricao,
        string prioridade, decimal? valorEstimado, DateTime? dataPrevista,
        string? tecnico, string? observacao,
        List<(string descricao, decimal quantidade, decimal valorUnitario)> itens);
    Task<(bool sucesso, string mensagem)> AtualizarStatusAsync(int id, string novoStatus);
    Task<(bool sucesso, string mensagem)> FinalizarAsync(int id, decimal valorFinal, string? observacao);
    Task<(bool sucesso, string mensagem)> CancelarAsync(int id);
}
