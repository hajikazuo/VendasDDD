using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Clientes;
using Vendas.Domain.Clientes.ValueObjects;

namespace Vendas.Application.Commands.ClientesCommands.CriarCliente;

public sealed class CriarClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public CriarClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<CriarClienteResultDto> HandleAsync(
            CriarClienteCommand command,
            CancellationToken cancellationToken = default)
    {
        var endereco = new Endereco(
            command.Cep,
            command.Logradouro,
            command.Numero,
            command.Bairro,
            command.Cidade,
            command.Estado,
            command.Pais,
            command.Complemento);

        var cliente = new Cliente(
            new NomeCompleto(command.NomeCompleto),
            new Cpf(command.Cpf),
            new Email(command.Email),
            new Telefone(command.Telefone),
            endereco,
            command.Sexo,
            command.EstadoCivil);

        await _clienteRepository.AdicionarAsync(cliente, cancellationToken);

        return new CriarClienteResultDto(
            cliente.Id,
            cliente.Nome.NomeCompletoFormatado,
            cliente.Email.Endereco);
    }
}
