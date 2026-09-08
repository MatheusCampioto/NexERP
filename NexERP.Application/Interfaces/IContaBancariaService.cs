using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IContaBancariaService
{
    Task<IEnumerable<ContaBancaria>> ListarTodosAsync();
    Task<ContaBancaria?> BuscarPorIdAsync(int id);
    Task<ContaBancaria> CriarAsync(string nome, string? banco, string? agencia, string? numeroConta, decimal saldoInicial);
    Task<bool> AtualizarAsync(int id, string nome, string? banco, string? agencia, string? numeroConta);
    Task<bool> DesativarAsync(int id);
}
