using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.ClientesCommands.AlterarEnderecoDoCliente;

public sealed class AlterarEnderecoDoClienteCommandHandler
{
    private readonly IClienteRepository _clienteRepository;

    public AlterarEnderecoDoClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<AlterarEnderecoDoClienteResultDto> HandleAsync(AlterarEnderecoDoClienteCommand command, CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(command.ClienteId, cancellationToken);

        Guard.AgainstNull(cliente, nameof(cliente), "Cliente não encontrado.");

        cliente!.AlterarEndereco(command.EnderecoId, command.Cep, command.Logradouro,
            command.Numero, command.Bairro, command.Cidade, command.Estado, command.Pais,
            command.Complemento);

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);

        return new AlterarEnderecoDoClienteResultDto(
            cliente.Id,
            command.EnderecoId);
    }
}