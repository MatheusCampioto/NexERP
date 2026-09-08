using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;
using NexERP.Domain.Enums;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
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
            return NotFound(new { mensagem = "Pedido nao encontrado." });
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest request)
    {
        var itens = request.Itens.Select(i => (i.ProdutoId, i.Quantidade, i.Desconto)).ToList();
        var resultado = await _pedidoService.CriarAsync(
            request.PessoaId, request.Observacao, request.CondicaoPagamentoId,
            request.FormaPagamento, request.Desconto, itens);
        if (!resultado.sucesso)
            return BadRequest(new { mensagem = resultado.mensagem });
        return StatusCode(201, resultado.pedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarPedidoRequest request)
    {
        var itens = request.Itens.Select(i => (i.ProdutoId, i.Quantidade, i.Desconto)).ToList();
        var resultado = await _pedidoService.AtualizarAsync(
            id, request.PessoaId, request.Observacao, request.CondicaoPagamentoId,
            request.FormaPagamento, request.Desconto, itens);
        if (!resultado.sucesso)
            return BadRequest(new { mensagem = resultado.mensagem });
        return NoContent();
    }

    [HttpPatch("{id}/avancar")]
    public async Task<IActionResult> Avancar(int id)
    {
        var resultado = await _pedidoService.AvancarStatusAsync(id);
        if (!resultado.sucesso)
            return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var resultado = await _pedidoService.CancelarAsync(id);
        if (!resultado.sucesso)
            return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }
}

public record CriarPedidoRequest(
    int PessoaId, string? Observacao, int? CondicaoPagamentoId,
    FormaPagamento? FormaPagamento, decimal Desconto, List<ItemPedidoRequest> Itens);

public record AtualizarPedidoRequest(
    int PessoaId, string? Observacao, int? CondicaoPagamentoId,
    FormaPagamento? FormaPagamento, decimal Desconto, List<ItemPedidoRequest> Itens);

public record ItemPedidoRequest(int ProdutoId, int Quantidade, decimal Desconto);
