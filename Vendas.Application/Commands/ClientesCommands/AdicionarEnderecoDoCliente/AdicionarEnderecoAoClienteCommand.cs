namespace Vendas.Application.Commands.ClientesCommands.AdicionarEnderecoDoCliente;

public sealed class AdicionarEnderecoAoClienteCommand
{
    public Guid ClienteId { get; }
    public string Cep { get; }
    public string Logradouro { get; }
    public string Numero { get; }
    public string Bairro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public string Pais { get; }
    public string Complemento { get; }

    public AdicionarEnderecoAoClienteCommand(
        Guid clienteId,
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string cidade,
        string estado,
        string pais,
        string complemento)
    {
        ClienteId = clienteId;
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Pais = pais;
        Complemento = complemento;
    }
}

