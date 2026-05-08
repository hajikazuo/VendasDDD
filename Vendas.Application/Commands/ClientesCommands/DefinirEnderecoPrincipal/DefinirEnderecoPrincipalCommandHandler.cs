using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.DefinirEnderecoPrincipal;

public sealed class DefinirEnderecoPrincipalCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public DefinirEnderecoPrincipalCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<DefinirEnderecoPrincipalResultDto> HandleAsync(
        DefinirEnderecoPrincipalCommand command,
        CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        cliente!.DefinirEnderecoPrincipal(command.EnderecoId);

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new DefinirEnderecoPrincipalResultDto(
            cliente.Id,
            command.EnderecoId);
    }
}
