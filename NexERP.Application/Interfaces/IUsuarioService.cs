using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> ListarTodosAsync();
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task<(bool sucesso, string mensagem)> AtualizarAsync(
        int id, string nome, string perfil, bool ativo,
        bool acessoPessoas, bool acessoProdutos, bool acessoEstoque,
        bool acessoPedidos, bool acessoFinanceiro, bool acessoRelatorios,
        bool acessoUsuarios);
    Task<(bool sucesso, string mensagem)> AlterarSenhaAsync(int id, string senhaAtual, string novaSenha);
    Task<(bool sucesso, string mensagem)> DesativarAsync(int id);
}
