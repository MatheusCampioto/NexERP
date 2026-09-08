namespace NexERP.Application.Interfaces;

public interface IAuthService
{
    Task<(bool sucesso, string mensagem, string? token)> LoginAsync(string email, string senha);
    Task RegistrarAsync(string nome, string email, string senha, string perfil = "Operador");
}
