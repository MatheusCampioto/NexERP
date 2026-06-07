using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class ContaBancariaService
{
    private readonly IContaBancariaRepository _contaRepository;

    public ContaBancariaService(IContaBancariaRepository contaRepository)
    {
        _contaRepository = contaRepository;
    }

    public async Task<IEnumerable<ContaBancaria>> ListarTodosAsync()
        => await _contaRepository.ListarTodosAsync();

    public async Task<ContaBancaria?> BuscarPorIdAsync(int id)
        => await _contaRepository.BuscarPorIdAsync(id);

    public async Task<ContaBancaria> CriarAsync(string nome, string? banco,
        string? agencia, string? numeroConta, decimal saldoInicial)
    {
        var conta = new ContaBancaria
        {
            Nome = nome,
            Banco = banco,
            Agencia = agencia,
            NumeroConta = numeroConta,
            SaldoInicial = saldoInicial,
            SaldoAtual = saldoInicial
        };

        await _contaRepository.AdicionarAsync(conta);
        await _contaRepository.SalvarAsync();
        return conta;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string? banco,
        string? agencia, string? numeroConta)
    {
        var conta = await _contaRepository.BuscarPorIdAsync(id);
        if (conta == null) return false;

        conta.Nome = nome;
        conta.Banco = banco;
        conta.Agencia = agencia;
        conta.NumeroConta = numeroConta;

        await _contaRepository.AtualizarAsync(conta);
        await _contaRepository.SalvarAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var conta = await _contaRepository.BuscarPorIdAsync(id);
        if (conta == null) return false;

        conta.Ativa = false;
        await _contaRepository.AtualizarAsync(conta);
        await _contaRepository.SalvarAsync();
        return true;
    }
}