using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.BloquearCliente;

public sealed class BloquearClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public BloquearClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<BloquearClienteResultDto> HandleAsync(
        BloquearClienteCommand command,
        CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        cliente!.Bloquear();

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new BloquearClienteResultDto(cliente.Id, cliente.Status);
    }
}
