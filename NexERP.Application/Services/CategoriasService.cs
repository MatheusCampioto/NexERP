using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Categoria>> ListarTodosAsync()
        => await _categoriaRepository.ListarTodosAsync();

    public async Task<Categoria> CriarAsync(string nome, string? descricao)
    {
        var categoria = new Categoria { Nome = nome, Descricao = descricao };
        await _categoriaRepository.AdicionarAsync(categoria);
        await _unitOfWork.CommitAsync();
        return categoria;
    }

    public async Task<bool> AtualizarAsync(int id, string nome, string? descricao)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null) return false;
        categoria.Nome = nome;
        categoria.Descricao = descricao;
        await _categoriaRepository.AtualizarAsync(categoria);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(id);
        if (categoria == null) return false;
        categoria.Ativa = false;
        await _categoriaRepository.AtualizarAsync(categoria);
        await _unitOfWork.CommitAsync();
        return true;
    }
}
