using System;
using System.ComponentModel.DataAnnotations;

namespace NexERP.Application.DTOs
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CnpjCpf { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public decimal LimiteCredito { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string Observacoes { get; set; }
    }

    public class CriarClienteDto
    {
        [Required(ErrorMessage = "Razão Social é obrigatória.")]
        [MaxLength(200, ErrorMessage = "Razão Social deve ter no máximo 200 caracteres.")]
        public string RazaoSocial { get; set; }

        [MaxLength(200)]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "CNPJ/CPF é obrigatório.")]
        [MaxLength(18, ErrorMessage = "CNPJ/CPF inválido.")]
        public string CnpjCpf { get; set; }

        [MaxLength(20)]
        public string InscricaoEstadual { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        [MaxLength(20)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório.")]
        [MaxLength(250)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Número é obrigatório.")]
        [MaxLength(10)]
        public string Numero { get; set; }

        [MaxLength(100)]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório.")]
        [MaxLength(100)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória.")]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "UF é obrigatória.")]
        [MaxLength(2, ErrorMessage = "UF deve ter 2 caracteres.")]
        public string UF { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório.")]
        [MaxLength(10)]
        public string CEP { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Limite de crédito não pode ser negativo.")]
        public decimal LimiteCredito { get; set; }

        [MaxLength(500)]
        public string Observacoes { get; set; }
    }

    public class AtualizarClienteDto
    {
        [Required(ErrorMessage = "Razão Social é obrigatória.")]
        [MaxLength(200)]
        public string RazaoSocial { get; set; }

        [MaxLength(200)]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        [MaxLength(20)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório.")]
        [MaxLength(250)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Número é obrigatório.")]
        [MaxLength(10)]
        public string Numero { get; set; }

        [MaxLength(100)]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório.")]
        [MaxLength(100)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória.")]
        [MaxLength(100)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "UF é obrigatória.")]
        [MaxLength(2)]
        public string UF { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório.")]
        [MaxLength(10)]
        public string CEP { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Limite de crédito não pode ser negativo.")]
        public decimal LimiteCredito { get; set; }

        [MaxLength(500)]
        public string Observacoes { get; set; }
    }
}