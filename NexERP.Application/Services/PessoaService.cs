using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class PessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task<IEnumerable<Pessoa>> ListarTodosAsync()
        => await _pessoaRepository.ListarTodosAsync();

    public async Task<Pessoa?> BuscarPorIdAsync(int id)
        => await _pessoaRepository.BuscarPorIdAsync(id);

    public async Task<Pessoa> CriarAsync(string nome, string tipo, string? cpfCnpj,
        string? email, string? telefone, string? endereco, string? cidade,
        string? estado, string? cep)
    {
        var pessoa = new Pessoa
        {
            Nome = nome,
            Tipo = tipo,
            CPF_CNPJ = cpfCnpj,
            Email = email,
            Telefone = telefone,
            Endereco = endereco,
            Cidade = cidade,
            Estado = estado,
            CEP = cep
        };

        await _pessoaRepository.AdicionarAsync(pessoa);
        await _pessoaRepository.SalvarAsync();
        return pessoa;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string tipo, string? cpfCnpj,
        string? email, string? telefone, string? endereco, string? cidade,
        string? estado, string? cep)
    {
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(id);
        if (pessoa == null) return false;

        pessoa.Nome = nome;
        pessoa.Tipo = tipo;
        pessoa.CPF_CNPJ = cpfCnpj;
        pessoa.Email = email;
        pessoa.Telefone = telefone;
        pessoa.Endereco = endereco;
        pessoa.Cidade = cidade;
        pessoa.Estado = estado;
        pessoa.CEP = cep;

        await _pessoaRepository.AtualizarAsync(pessoa);
        await _pessoaRepository.SalvarAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(id);
        if (pessoa == null) return false;

        pessoa.Ativo = false;
        await _pessoaRepository.AtualizarAsync(pessoa);
        await _pessoaRepository.SalvarAsync();
        return true;
    }
}