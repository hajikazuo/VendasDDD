using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Clientes;
using Vendas.Domain.Clientes.Entities;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.AdicionarEnderecoDoCliente;

public sealed class AdicionarEnderecoAoClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public AdicionarEnderecoAoClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<AdicionarEnderecoAoClienteResultDto> HandleAsync(
        AdicionarEnderecoAoClienteCommand command,
        CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        var endereco = new Endereco(
            command.Cep,
            command.Logradouro,
            command.Numero,
            command.Bairro,
            command.Cidade,
            command.Estado,
            command.Pais,
            command.Complemento);

        cliente!.AdicionarEndereco(endereco);

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new AdicionarEnderecoAoClienteResultDto(cliente.Id, endereco.Id);
    }
}
