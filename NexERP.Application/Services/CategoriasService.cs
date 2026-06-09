using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Categoria>> ListarTodosAsync()
        => await _categoriaRepository.ListarTodosAsync();

    public async Task<Categoria> CriarAsync(string nome, string? descricao)
    {
        var categoria = new Categoria { Nome = nome, Descricao = descricao };
        await _categoriaRepository.AdicionarAsync(categoria);
        await _categoriaRepository.SalvarAsync();
        return categoria;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string? descricao)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null) return false;
        categoria.Nome = nome;
        categoria.Descricao = descricao;
        await _categoriaRepository.AtualizarAsync(categoria);
        await _categoriaRepository.SalvarAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null) return false;
        categoria.Ativa = false;
        await _categoriaRepository.AtualizarAsync(categoria);
        await _categoriaRepository.SalvarAsync();
        return true;
    }
}