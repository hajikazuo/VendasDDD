namespace Vendas.Application.Commands.ClientesCommands.AlterarEnderecoDoCliente;

public sealed class AlterarEnderecoDoClienteResultDto
{
    public Guid ClienteId { get; }
    public Guid EnderecoId { get; }

    public AlterarEnderecoDoClienteResultDto(Guid clienteId, Guid enderecoId)
    {
        ClienteId = clienteId;
        EnderecoId = enderecoId;
    }
}