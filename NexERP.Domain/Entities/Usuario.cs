namespace NexERP.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string Perfil { get; set; } = "Operador"; // Admin, Gerente, Operador
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? UltimoAcesso { get; set; }

    // Permissões por módulo
    public bool AcessoPessoas { get; set; } = true;
    public bool AcessoProdutos { get; set; } = true;
    public bool AcessoEstoque { get; set; } = true;
    public bool AcessoPedidos { get; set; } = true;
    public bool AcessoFinanceiro { get; set; } = false;
    public bool AcessoRelatorios { get; set; } = false;
    public bool AcessoUsuarios { get; set; } = false;
}