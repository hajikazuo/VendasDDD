using Vendas.Domain.Clientes.Enums;

namespace Vendas.Application.Commands.ClientesCommands.AtualizarPerfilCliente;

public sealed class AtualizarPerfilClienteCommand
{
    public Guid ClienteId { get; }
    public string NomeCompleto { get; }
    public string Email { get; }
    public string Telefone { get; }
    public Sexo Sexo { get; }
    public EstadoCivil EstadoCivil { get; }

    public AtualizarPerfilClienteCommand(
        Guid clienteId,
        string nomeCompleto,
        string email,
        string telefone,
        Sexo sexo,
        EstadoCivil estadoCivil)
    {
        ClienteId = clienteId;
        NomeCompleto = nomeCompleto;
        Email = email;
        Telefone = telefone;
        Sexo = sexo;
        EstadoCivil = estadoCivil;
    }
}
