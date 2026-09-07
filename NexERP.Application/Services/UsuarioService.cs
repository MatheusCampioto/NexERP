using NexERP.Application.Interfaces;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        => await _usuarioRepository.ListarTodosAsync();

    public async Task<Usuario?> BuscarPorIdAsync(int id)
        => await _usuarioRepository.BuscarPorIdAsync(id);

    public async Task<(bool sucesso, string mensagem)> AtualizarAsync(
        int id, string nome, string perfil, bool ativo,
        bool acessoPessoas, bool acessoProdutos, bool acessoEstoque,
        bool acessoPedidos, bool acessoFinanceiro, bool acessoRelatorios,
        bool acessoUsuarios)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null) return (false, "Usuario nao encontrado.");

        usuario.Nome = nome;
        usuario.Perfil = perfil;
        usuario.Ativo = ativo;
        usuario.AcessoPessoas = acessoPessoas;
        usuario.AcessoProdutos = acessoProdutos;
        usuario.AcessoEstoque = acessoEstoque;
        usuario.AcessoPedidos = acessoPedidos;
        usuario.AcessoFinanceiro = acessoFinanceiro;
        usuario.AcessoRelatorios = acessoRelatorios;
        usuario.AcessoUsuarios = acessoUsuarios;

        await _usuarioRepository.AtualizarAsync(usuario);
        await _unitOfWork.CommitAsync();
        return (true, "Usuario atualizado com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> AlterarSenhaAsync(int id, string senhaAtual, string novaSenha)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null) return (false, "Usuario nao encontrado.");

        if (!BCrypt.Net.BCrypt.Verify(senhaAtual, usuario.SenhaHash))
            return (false, "Senha atual incorreta.");

        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
        await _unitOfWork.CommitAsync();
        return (true, "Senha alterada com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> DesativarAsync(int id)
    {
        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);
        if (usuario == null) return (false, "Usuario nao encontrado.");

        usuario.Ativo = false;
        await _unitOfWork.CommitAsync();
        return (true, "Usuario desativado.");
    }
}
