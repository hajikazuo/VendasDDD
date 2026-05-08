namespace Vendas.Application.Commands.ClientesCommands.AdicionarEnderecoDoCliente;

public sealed class AdicionarEnderecoAoClienteResultDto
{
    public Guid ClienteId { get; }
    public Guid EnderecoId { get; }

    public AdicionarEnderecoAoClienteResultDto(Guid clienteId, Guid enderecoId)
    {
        ClienteId = clienteId;
        EnderecoId = enderecoId;
    }
}
