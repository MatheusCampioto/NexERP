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
        var token = await _authService.LoginAsync(request.Email, request.Senha);

        if (token == null)
            return Unauthorized(new { mensagem = "Email ou senha inválidos." });

        return Ok(new { token });
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarRequest request)
    {
        await _authService.RegistrarAsync(request.Nome, request.Email, request.Senha, request.Perfil);
        return Created("", new { mensagem = "Usuário criado com sucesso." });
    }
}

public record LoginRequest(string Email, string Senha);
public record RegistrarRequest(string Nome, string Email, string Senha, string Perfil = "Operador");