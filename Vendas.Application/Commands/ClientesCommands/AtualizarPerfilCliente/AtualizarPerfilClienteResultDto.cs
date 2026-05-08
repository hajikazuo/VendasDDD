namespace Vendas.Application.Commands.ClientesCommands.AtualizarPerfilCliente;

public sealed class AtualizarPerfilClienteResultDto
{
    public Guid ClienteId { get; }

    public AtualizarPerfilClienteResultDto(Guid clienteId)
    {
        ClienteId = clienteId;
    }
}
