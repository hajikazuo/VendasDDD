using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.AtivarCliente;

public sealed class AtivarClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public AtivarClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<AtivarClienteResultDto> HandleAsync(
        AtivarClienteCommand command,
        CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        cliente!.Ativar();

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new AtivarClienteResultDto(cliente.Id, cliente.Status);
    }
}
