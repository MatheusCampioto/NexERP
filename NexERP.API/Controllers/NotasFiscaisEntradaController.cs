using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotasFiscaisEntradaController : ControllerBase
{
    private readonly NotaFiscalEntradaService _service;

    public NotasFiscaisEntradaController(NotaFiscalEntradaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _service.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var nf = await _service.BuscarPorIdAsync(id);
        if (nf == null) return NotFound(new { mensagem = "NF não encontrada." });
        return Ok(nf);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarNFEntradaRequest request)
    {
        var itens = request.Itens.Select(i => (i.ProdutoId, i.Descricao, i.Quantidade, i.ValorUnitario)).ToList();
        var nf = await _service.CriarAsync(request.OrdemCompraId, request.NumeroNF,
            request.Serie, request.ChaveAcesso, request.DataEmissao,
            request.ValorProdutos, request.ValorFrete, request.ValorImpostos,
            request.Observacao, itens);
        return StatusCode(201, nf);
    }

    [HttpPatch("{id}/entrada-estoque")]
    public async Task<IActionResult> DarEntradaEstoque(int id)
    {
        var (sucesso, mensagem) = await _service.DarEntradaEstoqueAsync(id);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }
}

public record CriarNFEntradaRequest(
    int OrdemCompraId,
    string NumeroNF,
    string? Serie,
    string? ChaveAcesso,
    DateTime DataEmissao,
    decimal ValorProdutos,
    decimal ValorFrete,
    decimal ValorImpostos,
    string? Observacao,
    List<ItemNFEntradaRequest> Itens
);

public record ItemNFEntradaRequest(
    int? ProdutoId,
    string Descricao,
    decimal Quantidade,
    decimal ValorUnitario
);