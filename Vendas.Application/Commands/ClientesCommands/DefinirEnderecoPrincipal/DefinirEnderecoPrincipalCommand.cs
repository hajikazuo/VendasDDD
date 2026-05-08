namespace Vendas.Application.Commands.ClientesCommands.DefinirEnderecoPrincipal;

public sealed class DefinirEnderecoPrincipalCommand
{
    public Guid ClienteId { get; }
    public Guid EnderecoId { get; }

    public DefinirEnderecoPrincipalCommand(Guid clienteId, Guid enderecoId)
    {
        ClienteId = clienteId;
        EnderecoId = enderecoId;
    }
}
