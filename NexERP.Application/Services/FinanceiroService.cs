using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class FinanceiroService
{
    private readonly ILancamentoFinanceiroRepository _lancamentoRepository;

    public FinanceiroService(ILancamentoFinanceiroRepository lancamentoRepository)
    {
        _lancamentoRepository = lancamentoRepository;
    }

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarTodosAsync()
        => await _lancamentoRepository.ListarTodosAsync();

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarContasAPagarAsync()
        => await _lancamentoRepository.ListarPorTipoAsync("Despesa");

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarContasAReceberAsync()
        => await _lancamentoRepository.ListarPorTipoAsync("Receita");

    public async Task<LancamentoFinanceiro?> BuscarPorIdAsync(int id)
        => await _lancamentoRepository.BuscarPorIdAsync(id);

    public async Task<LancamentoFinanceiro> CriarAsync(string tipo, string descricao,
        decimal valor, DateTime dataVencimento, string? categoria, int? pessoaId)
    {
        var lancamento = new LancamentoFinanceiro
        {
            Tipo = tipo,
            Descricao = descricao,
            Valor = valor,
            DataVencimento = DateTime.SpecifyKind(dataVencimento, DateTimeKind.Utc),
            Categoria = categoria,
            PessoaId = pessoaId
        };

        await _lancamentoRepository.AdicionarAsync(lancamento);
        await _lancamentoRepository.SalvarAsync();
        return lancamento;
    }

    public async Task<(bool sucesso, string mensagem)> BaixarAsync(int id)
    {
        var lancamento = await _lancamentoRepository.BuscarPorIdAsync(id);
        if (lancamento == null) return (false, "Lançamento não encontrado.");
        if (lancamento.Status == "Pago") return (false, "Lançamento já está pago.");
        if (lancamento.Status == "Cancelado") return (false, "Lançamento cancelado não pode ser baixado.");

        lancamento.Status = "Pago";
        lancamento.DataPagamento = DateTime.UtcNow;

        await _lancamentoRepository.AtualizarAsync(lancamento);
        await _lancamentoRepository.SalvarAsync();

        return (true, "Lançamento baixado com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> CancelarAsync(int id)
    {
        var lancamento = await _lancamentoRepository.BuscarPorIdAsync(id);
        if (lancamento == null) return (false, "Lançamento não encontrado.");
        if (lancamento.Status == "Pago") return (false, "Lançamento pago não pode ser cancelado.");

        lancamento.Status = "Cancelado";

        await _lancamentoRepository.AtualizarAsync(lancamento);
        await _lancamentoRepository.SalvarAsync();

        return (true, "Lançamento cancelado.");
    }
}