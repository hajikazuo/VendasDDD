using Vendas.Domain.Clientes.Enums;

namespace Vendas.Application.Commands.ClientesCommands.BloquearCliente;

public sealed class BloquearClienteResultDto
{
    public Guid ClienteId { get; }
    public StatusCliente Status { get; }

    public BloquearClienteResultDto(Guid clienteId, StatusCliente status)
    {
        ClienteId = clienteId;
        Status = status;
    }
}
