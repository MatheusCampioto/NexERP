using System;
using System.Collections.Generic;

namespace NexERP.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string CnpjCpf { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string CEP { get; private set; }
        public decimal LimiteCredito { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public string Observacoes { get; private set; }

        protected Cliente() { }

        public Cliente(
            string razaoSocial,
            string nomeFantasia,
            string cnpjCpf,
            string inscricaoEstadual,
            string email,
            string telefone,
            string celular,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cidade,
            string uf,
            string cep,
            decimal limiteCredito,
            string observacoes = null)
        {
            Id = Guid.NewGuid();
            RazaoSocial = razaoSocial ?? throw new ArgumentNullException(nameof(razaoSocial));
            NomeFantasia = nomeFantasia;
            CnpjCpf = cnpjCpf ?? throw new ArgumentNullException(nameof(cnpjCpf));
            InscricaoEstadual = inscricaoEstadual;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Telefone = telefone;
            Celular = celular;
            Logradouro = logradouro ?? throw new ArgumentNullException(nameof(logradouro));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            Complemento = complemento;
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro));
            Cidade = cidade ?? throw new ArgumentNullException(nameof(cidade));
            UF = uf ?? throw new ArgumentNullException(nameof(uf));
            CEP = cep ?? throw new ArgumentNullException(nameof(cep));
            LimiteCredito = limiteCredito >= 0 ? limiteCredito : throw new ArgumentException("Limite de crédito não pode ser negativo.");
            Observacoes = observacoes;
            Ativo = true;
            DataCadastro = DateTime.UtcNow;
        }

        public void Atualizar(
            string razaoSocial,
            string nomeFantasia,
            string email,
            string telefone,
            string celular,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cidade,
            string uf,
            string cep,
            decimal limiteCredito,
            string observacoes = null)
        {
            RazaoSocial = razaoSocial ?? throw new ArgumentNullException(nameof(razaoSocial));
            NomeFantasia = nomeFantasia;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Telefone = telefone;
            Celular = celular;
            Logradouro = logradouro ?? throw new ArgumentNullException(nameof(logradouro));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            Complemento = complemento;
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro));
            Cidade = cidade ?? throw new ArgumentNullException(nameof(cidade));
            UF = uf ?? throw new ArgumentNullException(nameof(uf));
            CEP = cep ?? throw new ArgumentNullException(nameof(cep));
            LimiteCredito = limiteCredito >= 0 ? limiteCredito : throw new ArgumentException("Limite de crédito não pode ser negativo.");
            Observacoes = observacoes;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            if (!Ativo) throw new InvalidOperationException("Cliente já está inativo.");
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Ativo) throw new InvalidOperationException("Cliente já está ativo.");
            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarLimiteCredito(decimal novoLimite)
        {
            if (novoLimite < 0) throw new ArgumentException("Limite de crédito não pode ser negativo.");
            LimiteCredito = novoLimite;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}