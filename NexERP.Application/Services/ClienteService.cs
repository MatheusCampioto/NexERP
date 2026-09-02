using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NexERP.Application.DTOs;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
        {
            var clientes = await _clienteRepository.ObterTodosAsync();
            return clientes.Select(MapearParaDto);
        }

        public async Task<IEnumerable<ClienteDto>> ObterAtivosAsync()
        {
            var clientes = await _clienteRepository.ObterAtivosAsync();
            return clientes.Select(MapearParaDto);
        }

        public async Task<ClienteDto> ObterPorIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            return MapearParaDto(cliente);
        }

        public async Task<ClienteDto> ObterPorCnpjCpfAsync(string cnpjCpf)
        {
            if (string.IsNullOrWhiteSpace(cnpjCpf))
                throw new ArgumentException("CNPJ/CPF não pode ser vazio.");
            var cliente = await _clienteRepository.ObterPorCnpjCpfAsync(cnpjCpf);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com CNPJ/CPF {cnpjCpf} não encontrado.");
            return MapearParaDto(cliente);
        }

        public async Task<IEnumerable<ClienteDto>> BuscarPorRazaoSocialAsync(string razaoSocial)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial))
                throw new ArgumentException("Razão Social não pode ser vazia.");
            var clientes = await _clienteRepository.BuscarPorRazaoSocialAsync(razaoSocial);
            return clientes.Select(MapearParaDto);
        }

        public async Task<ClienteDto> CriarAsync(CriarClienteDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var jaExiste = await _clienteRepository.ExistePorCnpjCpfAsync(dto.CnpjCpf);
            if (jaExiste)
                throw new InvalidOperationException($"Já existe um cliente com o CNPJ/CPF {dto.CnpjCpf}.");

            var cliente = new Cliente(
                dto.RazaoSocial,
                dto.NomeFantasia,
                dto.CnpjCpf,
                dto.InscricaoEstadual,
                dto.Email,
                dto.Telefone,
                dto.Celular,
                dto.Logradouro,
                dto.Numero,
                dto.Complemento,
                dto.Bairro,
                dto.Cidade,
                dto.UF,
                dto.CEP,
                dto.LimiteCredito,
                dto.Observacoes
            );

            await _clienteRepository.AdicionarAsync(cliente);
            return MapearParaDto(cliente);
        }

        public async Task<ClienteDto> AtualizarAsync(Guid id, AtualizarClienteDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

            cliente.Atualizar(
                dto.RazaoSocial,
                dto.NomeFantasia,
                dto.Email,
                dto.Telefone,
                dto.Celular,
                dto.Logradouro,
                dto.Numero,
                dto.Complemento,
                dto.Bairro,
                dto.Cidade,
                dto.UF,
                dto.CEP,
                dto.LimiteCredito,
                dto.Observacoes
            );

            await _clienteRepository.AtualizarAsync(cliente);
            return MapearParaDto(cliente);
        }

        public async Task AtualizarLimiteCreditoAsync(Guid id, decimal novoLimite)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

            cliente.AtualizarLimiteCredito(novoLimite);
            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task DesativarAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

            cliente.Desativar();
            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task AtivarAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

            cliente.Ativar();
            await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");

            await _clienteRepository.RemoverAsync(id);
        }

        public async Task<int> TotalClientesAtivosAsync()
        {
            return await _clienteRepository.TotalClientesAtivosAsync();
        }

        private static ClienteDto MapearParaDto(Cliente cliente) => new ClienteDto
        {
            Id = cliente.Id,
            RazaoSocial = cliente.RazaoSocial,
            NomeFantasia = cliente.NomeFantasia,
            CnpjCpf = cliente.CnpjCpf,
            InscricaoEstadual = cliente.InscricaoEstadual,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Celular = cliente.Celular,
            Logradouro = cliente.Logradouro,
            Numero = cliente.Numero,
            Complemento = cliente.Complemento,
            Bairro = cliente.Bairro,
            Cidade = cliente.Cidade,
            UF = cliente.UF,
            CEP = cliente.CEP,
            LimiteCredito = cliente.LimiteCredito,
            Ativo = cliente.Ativo,
            DataCadastro = cliente.DataCadastro,
            DataAtualizacao = cliente.DataAtualizacao,
            Observacoes = cliente.Observacoes
        };
    }
}