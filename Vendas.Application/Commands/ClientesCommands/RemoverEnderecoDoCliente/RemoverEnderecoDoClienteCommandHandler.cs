using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.RemoverEnderecoDoCliente;

public sealed class RemoverEnderecoDoClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public RemoverEnderecoDoClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<RemoverEnderecoDoClienteResultDto> HandleAsync(
        RemoverEnderecoDoClienteCommand command,
        CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        cliente!.RemoverEndereco(command.EnderecoId);

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new RemoverEnderecoDoClienteResultDto(
            cliente.Id,
            command.EnderecoId);
    }
}
