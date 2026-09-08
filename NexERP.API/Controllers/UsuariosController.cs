using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;
using System.Security.Claims;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var usuarios = await _usuarioService.ListarTodosAsync();
        return Ok(usuarios.Select(u => new
        {
            u.Id, u.Nome, u.Email, u.Perfil, u.Ativo,
            u.CriadoEm, u.UltimoAcesso,
            u.AcessoPessoas, u.AcessoProdutos, u.AcessoEstoque,
            u.AcessoPedidos, u.AcessoFinanceiro, u.AcessoRelatorios, u.AcessoUsuarios
        }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var usuario = await _usuarioService.BuscarPorIdAsync(id);
        if (usuario == null)
            return NotFound(new { mensagem = "Usuario nao encontrado." });
        return Ok(new
        {
            usuario.Id, usuario.Nome, usuario.Email, usuario.Perfil, usuario.Ativo,
            usuario.CriadoEm, usuario.UltimoAcesso,
            usuario.AcessoPessoas, usuario.AcessoProdutos, usuario.AcessoEstoque,
            usuario.AcessoPedidos, usuario.AcessoFinanceiro, usuario.AcessoRelatorios,
            usuario.AcessoUsuarios
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarUsuarioRequest request)
    {
        var resultado = await _usuarioService.AtualizarAsync(
            id, request.Nome, request.Perfil, request.Ativo,
            request.AcessoPessoas, request.AcessoProdutos, request.AcessoEstoque,
            request.AcessoPedidos, request.AcessoFinanceiro, request.AcessoRelatorios,
            request.AcessoUsuarios);
        if (!resultado.sucesso) return NotFound(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/senha")]
    public async Task<IActionResult> AlterarSenha(int id, [FromBody] AlterarSenhaRequest request)
    {
        var resultado = await _usuarioService.AlterarSenhaAsync(id, request.SenhaAtual, request.NovaSenha);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPut("alterar-senha")]
    public async Task<IActionResult> AlterarSenhaPropria([FromBody] AlterarSenhaRequest request)
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idClaim == null) return Unauthorized();
        var id = int.Parse(idClaim);
        var resultado = await _usuarioService.AlterarSenhaAsync(id, request.SenhaAtual, request.NovaSenha);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var resultado = await _usuarioService.DesativarAsync(id);
        if (!resultado.sucesso) return NotFound(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }
}

public record AtualizarUsuarioRequest(
    string Nome, string Perfil, bool Ativo,
    bool AcessoPessoas, bool AcessoProdutos, bool AcessoEstoque,
    bool AcessoPedidos, bool AcessoFinanceiro, bool AcessoRelatorios,
    bool AcessoUsuarios);

public record AlterarSenhaRequest(string SenhaAtual, string NovaSenha);
