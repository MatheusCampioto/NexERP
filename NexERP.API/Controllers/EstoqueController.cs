using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EstoqueController : ControllerBase
{
    private readonly EstoqueService _estoqueService;

    public EstoqueController(EstoqueService estoqueService)
    {
        _estoqueService = estoqueService;
    }

    [HttpGet("{produtoId}")]
    public async Task<IActionResult> ListarMovimentacoes(int produtoId)
        => Ok(await _estoqueService.ListarMovimentacoesPorProdutoAsync(produtoId));

    [HttpGet("baixo-estoque")]
    public async Task<IActionResult> EstoqueBaixo()
        => Ok(await _estoqueService.ListarProdutosEstoqueBaixoAsync());

    [HttpPost]
    public async Task<IActionResult> Movimentar([FromBody] MovimentacaoRequest request)
    {
        var (sucesso, mensagem) = await _estoqueService.MovimentarAsync(
            request.ProdutoId, request.Tipo, request.Quantidade, request.Observacao);

        if (!sucesso)
            return BadRequest(new { mensagem });

        return StatusCode(201, new { mensagem });
    }

    [HttpPost("inventario")]
    public async Task<IActionResult> AjustarInventario([FromBody] InventarioRequest request)
    {
        var (sucesso, mensagem) = await _estoqueService.AjustarInventarioAsync(
            request.ProdutoId, request.QuantidadeReal, request.Observacao);

        if (!sucesso)
            return BadRequest(new { mensagem });

        return Ok(new { mensagem });
    }
}

public record MovimentacaoRequest(int ProdutoId, string Tipo, int Quantidade, string? Observacao);
public record InventarioRequest(int ProdutoId, int QuantidadeReal, string? Observacao);