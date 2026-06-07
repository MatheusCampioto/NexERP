using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _produtoService.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var produto = await _produtoService.BuscarPorIdAsync(id);
        if (produto == null)
            return NotFound(new { mensagem = "Produto não encontrado." });
        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ProdutoRequest request)
    {
        var produto = await _produtoService.CriarAsync(
            request.Nome, request.Descricao, request.Codigo,
            request.PrecoVenda, request.PrecoCusto, request.Unidade,
            request.Categoria, request.EstoqueMinimo);
        return StatusCode(201, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoRequest request)
    {
        var atualizado = await _produtoService.AtualizarAsync(
            id, request.Nome, request.Descricao, request.Codigo,
            request.PrecoVenda, request.PrecoCusto, request.Unidade,
            request.Categoria, request.EstoqueMinimo);

        if (!atualizado)
            return NotFound(new { mensagem = "Produto não encontrado." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var desativado = await _produtoService.DesativarAsync(id);
        if (!desativado)
            return NotFound(new { mensagem = "Produto não encontrado." });
        return NoContent();
    }
}

public record ProdutoRequest(
    string Nome,
    string? Descricao,
    string? Codigo,
    decimal PrecoVenda,
    decimal PrecoCusto,
    string? Unidade,
    string? Categoria,
    int EstoqueMinimo
);