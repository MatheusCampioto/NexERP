using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _categoriaService.ListarTodosAsync());

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CategoriaRequest request)
    {
        var categoria = await _categoriaService.CriarAsync(request.Nome, request.Descricao);
        return StatusCode(201, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CategoriaRequest request)
    {
        var atualizado = await _categoriaService.AtualizarAsync(id, request.Nome, request.Descricao);
        if (!atualizado) return NotFound(new { mensagem = "Categoria nao encontrada." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var desativado = await _categoriaService.DesativarAsync(id);
        if (!desativado) return NotFound(new { mensagem = "Categoria nao encontrada." });
        return NoContent();
    }
}

public record CategoriaRequest(string Nome, string? Descricao);
