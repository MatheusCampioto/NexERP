using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _pedidoService.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var pedido = await _pedidoService.BuscarPorIdAsync(id);
        if (pedido == null)
            return NotFound(new { mensagem = "Pedido não encontrado." });
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest request)
    {
        var itens = request.Itens.Select(i => (i.ProdutoId, i.Quantidade, i.Desconto)).ToList();
        var (sucesso, mensagem, pedido) = await _pedidoService.CriarAsync(
            request.PessoaId, request.Observacao, request.CondicaoPagamento,
            request.FormaPagamento, request.Desconto, itens);

        if (!sucesso)
            return BadRequest(new { mensagem });

        return StatusCode(201, pedido);
    }

    [HttpPatch("{id}/avancar")]
    public async Task<IActionResult> Avancar(int id)
    {
        var (sucesso, mensagem) = await _pedidoService.AvancarStatusAsync(id);
        if (!sucesso)
            return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var (sucesso, mensagem) = await _pedidoService.CancelarAsync(id);
        if (!sucesso)
            return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }
}

public record CriarPedidoRequest(
    int PessoaId,
    string? Observacao,
    string? CondicaoPagamento,
    string? FormaPagamento,
    decimal Desconto,
    List<ItemPedidoRequest> Itens
);

public record ItemPedidoRequest(int ProdutoId, int Quantidade, decimal Desconto);