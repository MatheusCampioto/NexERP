using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IFinanceiroService
{
    Task<IEnumerable<LancamentoFinanceiro>> ListarTodosAsync();
    Task<IEnumerable<LancamentoFinanceiro>> ListarContasAPagarAsync();
    Task<IEnumerable<LancamentoFinanceiro>> ListarContasAReceberAsync();
    Task<LancamentoFinanceiro?> BuscarPorIdAsync(int id);
    Task<IEnumerable<LancamentoFinanceiro>> CriarAsync(string tipo, string descricao,
        decimal valor, DateTime dataVencimento, string? categoria, int? pessoaId,
        string? formaPagamento, int? contaBancariaId, int totalParcelas = 1);
    Task<(bool sucesso, string mensagem)> BaixarAsync(int id);
    Task<(bool sucesso, string mensagem)> CancelarAsync(int id);
    Task<object> FluxoDeCaixaAsync(DateTime inicio, DateTime fim);
}
