using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<IEnumerable<Cliente>> ObterAtivosAsync();
        Task<Cliente> ObterPorCnpjCpfAsync(string cnpjCpf);
        Task<IEnumerable<Cliente>> BuscarPorRazaoSocialAsync(string razaoSocial);
        Task<IEnumerable<Cliente>> ObterPorCidadeAsync(string cidade);
        Task<IEnumerable<Cliente>> ObterPorUFAsync(string uf);
        Task<bool> ExistePorCnpjCpfAsync(string cnpjCpf, Guid? ignorarId = null);
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task RemoverAsync(Guid id);
        Task<int> TotalClientesAtivosAsync();
        Task<IEnumerable<Cliente>> ObterComLimiteCreditoAcimaDeAsync(decimal valor);
    }
}