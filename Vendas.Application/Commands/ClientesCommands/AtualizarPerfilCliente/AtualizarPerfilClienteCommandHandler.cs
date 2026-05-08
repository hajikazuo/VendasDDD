using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Clientes.ValueObjects;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.AtualizarPerfilCliente;

public sealed class AtualizarPerfilClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public AtualizarPerfilClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<AtualizarPerfilClienteResultDto> HandleAsync(
        AtualizarPerfilClienteCommand command,
        CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        cliente!.AtualizarPerfil(
            new NomeCompleto(command.NomeCompleto),
            new Email(command.Email),
            new Telefone(command.Telefone),
            command.Sexo,
            command.EstadoCivil);

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new AtualizarPerfilClienteResultDto(cliente.Id);
    }
}
