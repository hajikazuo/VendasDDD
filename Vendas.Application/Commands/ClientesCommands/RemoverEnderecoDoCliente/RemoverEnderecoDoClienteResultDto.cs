namespace Vendas.Application.Commands.ClientesCommands.RemoverEnderecoDoCliente;

public sealed class RemoverEnderecoDoClienteResultDto
{
    public Guid ClienteId { get; }
    public Guid EnderecoRemovidoId { get; }

    public RemoverEnderecoDoClienteResultDto(Guid clienteId, Guid enderecoRemovidoId)
    {
        ClienteId = clienteId;
        EnderecoRemovidoId = enderecoRemovidoId;
    }
}
