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
        var produto = await _produtoService.CriarAsync(request.ToDto());
        return StatusCode(201, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoRequest request)
    {
        var atualizado = await _produtoService.AtualizarAsync(id, request.ToDto());
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
    string? CodigoBarras,
    decimal PrecoVenda,
    decimal PrecoCusto,
    decimal? PrecoMinimo,
    string? Unidade,
    int? CategoriaId,
    int? FornecedorId,
    int EstoqueMinimo,
    int? EstoqueMaximo,
    string? LocalizacaoEstoque,
    bool ControlaValidade,
    int? DiasValidade,
    string? NCM,
    string? CEST,
    string? CFOP,
    string? OrigemMercadoria,
    string? CSOSN,
    string? CST_ICMS,
    string? CST_PIS,
    string? CST_COFINS,
    decimal? AliquotaICMS,
    decimal? AliquotaIPI,
    decimal? AliquotaPIS,
    decimal? AliquotaCOFINS,
    decimal? PesoBruto,
    decimal? PesoLiquido,
    decimal? Altura,
    decimal? Largura,
    decimal? Comprimento
)
{
    public ProdutoDto ToDto() => new(
        Nome, Descricao, Codigo, CodigoBarras,
        PrecoVenda, PrecoCusto, PrecoMinimo,
        Unidade, CategoriaId, FornecedorId,
        EstoqueMinimo, EstoqueMaximo, LocalizacaoEstoque,
        ControlaValidade, DiasValidade,
        NCM, CEST, CFOP, OrigemMercadoria,
        CSOSN, CST_ICMS, CST_PIS, CST_COFINS,
        AliquotaICMS, AliquotaIPI, AliquotaPIS, AliquotaCOFINS,
        PesoBruto, PesoLiquido, Altura, Largura, Comprimento
    );
}