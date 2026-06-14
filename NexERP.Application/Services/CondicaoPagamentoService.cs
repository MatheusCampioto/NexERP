using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class CondicaoPagamentoService
{
    private readonly ICondicaoPagamentoRepository _repository;

    public CondicaoPagamentoService(ICondicaoPagamentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CondicaoPagamento>> ListarTodosAsync()
        => await _repository.ListarTodosAsync();

    public async Task<CondicaoPagamento> CriarAsync(string nome, string? descricao,
        int numeroParcelas, int diasEntreParcelas, int primeiroPagamentoDias)
    {
        var condicao = new CondicaoPagamento
        {
            Nome = nome,
            Descricao = descricao,
            NumeroParcelas = numeroParcelas,
            DiasEntreParcelas = diasEntreParcelas,
            PrimeiroPagamentoDias = primeiroPagamentoDias
        };
        await _repository.AdicionarAsync(condicao);
        await _repository.SalvarAsync();
        return condicao;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string? descricao,
        int numeroParcelas, int diasEntreParcelas, int primeiroPagamentoDias)
    {
        var condicao = await _repository.BuscarPorIdAsync(id);
        if (condicao == null) return false;
        condicao.Nome = nome;
        condicao.Descricao = descricao;
        condicao.NumeroParcelas = numeroParcelas;
        condicao.DiasEntreParcelas = diasEntreParcelas;
        condicao.PrimeiroPagamentoDias = primeiroPagamentoDias;
        await _repository.AtualizarAsync(condicao);
        await _repository.SalvarAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var condicao = await _repository.BuscarPorIdAsync(id);
        if (condicao == null) return false;
        condicao.Ativa = false;
        await _repository.AtualizarAsync(condicao);
        await _repository.SalvarAsync();
        return true;
    }
}