namespace Vendas.Application.Commands.ClientesCommands.DefinirEnderecoPrincipal;

public sealed class DefinirEnderecoPrincipalResultDto
{
    public Guid ClienteId { get; }
    public Guid EnderecoPrincipalId { get; }

    public DefinirEnderecoPrincipalResultDto(Guid clienteId, Guid enderecoPrincipalId)
    {
        ClienteId = clienteId;
        EnderecoPrincipalId = enderecoPrincipalId;
    }
}

