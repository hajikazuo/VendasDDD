namespace Vendas.Application.Commands.ClientesCommands.BloquearCliente;

public sealed class BloquearClienteCommand
{
    public Guid ClienteId { get; }

    public BloquearClienteCommand(Guid clienteId)
    {
        ClienteId = clienteId;
    }
}
