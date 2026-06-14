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

    public async Task<Pessoa> CriarAsync(PessoaDto dto)
    {
        var pessoa = MapearDto(new Pessoa(), dto);
        await _pessoaRepository.AdicionarAsync(pessoa);
        await _pessoaRepository.SalvarAsync();
        return pessoa;
    }

    public async Task<bool> AtualizarAsync(int id, PessoaDto dto)
    {
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(id);
        if (pessoa == null) return false;
        MapearDto(pessoa, dto);
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

    private static Pessoa MapearDto(Pessoa pessoa, PessoaDto dto)
    {
        pessoa.TipoDocumento = dto.TipoDocumento;
        pessoa.Tipo = dto.Tipo;
        pessoa.Funcao = dto.Funcao;

        pessoa.Nome = dto.Nome ?? string.Empty;
        pessoa.CPF = dto.CPF;
        pessoa.RG = dto.RG;
        pessoa.DataNascimento = dto.DataNascimento.HasValue
            ? DateTime.SpecifyKind(dto.DataNascimento.Value, DateTimeKind.Utc)
            : null;
        pessoa.EstadoCivil = dto.EstadoCivil;
        pessoa.Profissao = dto.Profissao;

        pessoa.RazaoSocial = dto.RazaoSocial;
        pessoa.NomeFantasia = dto.NomeFantasia;
        pessoa.CNPJ = dto.CNPJ;
        pessoa.InscricaoEstadual = dto.InscricaoEstadual;
        pessoa.InscricaoMunicipal = dto.InscricaoMunicipal;
        pessoa.NomeContato = dto.NomeContato;
        pessoa.Site = dto.Site;

        pessoa.Email = dto.Email;
        pessoa.Telefone = dto.Telefone;
        pessoa.Celular = dto.Celular;

        pessoa.CEP = dto.CEP;
        pessoa.Endereco = dto.Endereco;
        pessoa.Numero = dto.Numero;
        pessoa.Complemento = dto.Complemento;
        pessoa.Bairro = dto.Bairro;
        pessoa.Cidade = dto.Cidade;
        pessoa.Estado = dto.Estado;

        pessoa.Observacao = dto.Observacao;
        return pessoa;
    }
}

public record PessoaDto(
    string TipoDocumento,
    string Tipo,
    string? Funcao,
    string? Nome,
    string? CPF,
    string? RG,
    DateTime? DataNascimento,
    string? EstadoCivil,
    string? Profissao,
    string? RazaoSocial,
    string? NomeFantasia,
    string? CNPJ,
    string? InscricaoEstadual,
    string? InscricaoMunicipal,
    string? NomeContato,
    string? Site,
    string? Email,
    string? Telefone,
    string? Celular,
    string? CEP,
    string? Endereco,
    string? Numero,
    string? Complemento,
    string? Bairro,
    string? Cidade,
    string? Estado,
    string? Observacao
);