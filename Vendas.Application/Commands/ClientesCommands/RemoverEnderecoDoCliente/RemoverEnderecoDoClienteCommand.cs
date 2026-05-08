namespace Vendas.Application.Commands.ClientesCommands.RemoverEnderecoDoCliente;

public sealed class RemoverEnderecoDoClienteCommand
{
    public Guid ClienteId { get; }
    public Guid EnderecoId { get; }

    public RemoverEnderecoDoClienteCommand(Guid clienteId, Guid enderecoId)
    {
        ClienteId = clienteId;
        EnderecoId = enderecoId;
    }
}
