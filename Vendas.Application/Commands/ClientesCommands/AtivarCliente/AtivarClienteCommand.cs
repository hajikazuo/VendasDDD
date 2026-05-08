namespace Vendas.Application.Commands.ClientesCommands.AtivarCliente;

public sealed class AtivarClienteCommand
{
    public Guid ClienteId { get; }

    public AtivarClienteCommand(Guid clienteId)
    {
        ClienteId = clienteId;
    }
}
