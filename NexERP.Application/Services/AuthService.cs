using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class AuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<string?> LoginAsync(string email, string senha)
    {
        var usuario = await _usuarioRepository.BuscarPorEmailAsync(email);

        if (usuario == null || !usuario.Ativo)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            return null;

        return GerarToken(usuario);
    }

    public async Task RegistrarAsync(string nome, string email, string senha, string perfil = "Operador")
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            SenhaHash = senhaHash,
            Perfil = perfil
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        await _usuarioRepository.SalvarAsync();
    }

    private string GerarToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Perfil)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}