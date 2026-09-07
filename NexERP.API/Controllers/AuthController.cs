using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var resultado = await _authService.LoginAsync(request.Email, request.Senha);

        if (!resultado.sucesso)
            return Unauthorized(new { mensagem = resultado.mensagem });

        return Ok(new { token = resultado.token });
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarRequest request)
    {
        await _authService.RegistrarAsync(request.Nome, request.Email, request.Senha, request.Perfil);
        return StatusCode(201, new { mensagem = "Usuário criado com sucesso." });
    }
}

public record LoginRequest(string Email, string Senha);
public record RegistrarRequest(string Nome, string Email, string Senha, string Perfil = "Operador");