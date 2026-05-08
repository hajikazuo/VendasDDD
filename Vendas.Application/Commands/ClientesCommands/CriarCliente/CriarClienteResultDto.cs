namespace Vendas.Application.Commands.ClientesCommands.CriarCliente;

public sealed class CriarClienteResultDto
{
    public Guid ClienteId { get; }
    public string Nome { get; }
    public string Email { get; }

    public CriarClienteResultDto(Guid clienteId, string nome, string email)
    {
        ClienteId = clienteId;
        Nome = nome;
        Email = email;
    }
}