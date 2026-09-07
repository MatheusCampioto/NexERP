using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IPessoaRepository : IRepository<Pessoa>
{
    Task<IEnumerable<Pessoa>> ListarPorTipoAsync(string tipo);
    Task<Pessoa?> BuscarPorCpfAsync(string cpf);
    Task<Pessoa?> BuscarPorCnpjAsync(string cnpj);
}