using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class FinanceiroService
{
    private readonly ILancamentoFinanceiroRepository _lancamentoRepository;
    private readonly IContaBancariaRepository _contaRepository;

    public FinanceiroService(ILancamentoFinanceiroRepository lancamentoRepository,
        IContaBancariaRepository contaRepository)
    {
        _lancamentoRepository = lancamentoRepository;
        _contaRepository = contaRepository;
    }

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarTodosAsync()
        => await _lancamentoRepository.ListarTodosAsync();

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarContasAPagarAsync()
        => await _lancamentoRepository.ListarPorTipoAsync("Despesa");

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarContasAReceberAsync()
        => await _lancamentoRepository.ListarPorTipoAsync("Receita");

    public async Task<LancamentoFinanceiro?> BuscarPorIdAsync(int id)
        => await _lancamentoRepository.BuscarPorIdAsync(id);

    public async Task<IEnumerable<LancamentoFinanceiro>> CriarAsync(string tipo, string descricao,
        decimal valor, DateTime dataVencimento, string? categoria, int? pessoaId,
        string? formaPagamento, int? contaBancariaId, int totalParcelas = 1)
    {
        var grupo = totalParcelas > 1 ? Guid.NewGuid().ToString("N")[..8] : null;
        var lancamentos = new List<LancamentoFinanceiro>();

        for (int i = 0; i < totalParcelas; i++)
        {
            var lancamento = new LancamentoFinanceiro
            {
                Tipo = tipo,
                Descricao = totalParcelas > 1 ? $"{descricao} ({i + 1}/{totalParcelas})" : descricao,
                Valor = Math.Round(valor / totalParcelas, 2),
                DataVencimento = DateTime.SpecifyKind(dataVencimento.AddMonths(i), DateTimeKind.Utc),
                Categoria = categoria,
                PessoaId = pessoaId,
                FormaPagamento = formaPagamento,
                ContaBancariaId = contaBancariaId,
                NumeroParcela = i + 1,
                TotalParcelas = totalParcelas,
                GrupoParcela = grupo
            };

            await _lancamentoRepository.AdicionarAsync(lancamento);
            lancamentos.Add(lancamento);
        }

        await _lancamentoRepository.SalvarAsync();
        return lancamentos;
    }

    public async Task<(bool sucesso, string mensagem)> BaixarAsync(int id)
    {
        var lancamento = await _lancamentoRepository.BuscarPorIdAsync(id);
        if (lancamento == null) return (false, "Lançamento não encontrado.");
        if (lancamento.Status == "Pago") return (false, "Lançamento já está pago.");
        if (lancamento.Status == "Cancelado") return (false, "Lançamento cancelado não pode ser baixado.");

        lancamento.Status = "Pago";
        lancamento.DataPagamento = DateTime.UtcNow;

        if (lancamento.ContaBancariaId.HasValue)
        {
            var conta = await _contaRepository.BuscarPorIdAsync(lancamento.ContaBancariaId.Value);
            if (conta != null)
            {
                if (lancamento.Tipo == "Receita")
                    conta.SaldoAtual += lancamento.Valor;
                else
                    conta.SaldoAtual -= lancamento.Valor;

                await _contaRepository.AtualizarAsync(conta);
            }
        }

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

    public async Task<object> FluxoDeCaixaAsync(DateTime inicio, DateTime fim)
    {
        var lancamentos = await _lancamentoRepository.ListarTodosAsync();
        var periodo = lancamentos.Where(l =>
            l.DataVencimento >= inicio && l.DataVencimento <= fim).ToList();

        var receitas = periodo.Where(l => l.Tipo == "Receita").Sum(l => l.Valor);
        var despesas = periodo.Where(l => l.Tipo == "Despesa").Sum(l => l.Valor);
        var receitasPagas = periodo.Where(l => l.Tipo == "Receita" && l.Status == "Pago").Sum(l => l.Valor);
        var despesasPagas = periodo.Where(l => l.Tipo == "Despesa" && l.Status == "Pago").Sum(l => l.Valor);

        var porCategoria = periodo
            .GroupBy(l => l.Categoria ?? "Sem categoria")
            .Select(g => new
            {
                categoria = g.Key,
                total = g.Sum(l => l.Valor),
                tipo = g.First().Tipo
            });

        return new
        {
            receitas,
            despesas,
            saldoPrevisto = receitas - despesas,
            receitasPagas,
            despesasPagas,
            saldoRealizado = receitasPagas - despesasPagas,
            porCategoria
        };
    }
}