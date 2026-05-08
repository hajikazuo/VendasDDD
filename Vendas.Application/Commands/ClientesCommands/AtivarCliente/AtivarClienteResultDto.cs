using Vendas.Domain.Clientes.Enums;

namespace Vendas.Application.Commands.ClientesCommands.AtivarCliente;

public sealed class AtivarClienteResultDto
{
    public Guid ClienteId { get; }
    public StatusCliente Status { get; }

    public AtivarClienteResultDto(Guid clienteId, StatusCliente status)
    {
        ClienteId = clienteId;
        Status = status;
    }
}

