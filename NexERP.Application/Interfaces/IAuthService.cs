namespace NexERP.Application.Interfaces;

public interface IAuthService
{
    Task<(bool sucesso, string mensagem, string? token)> LoginAsync(string email, string senha);
}