namespace Vendas.Application.Commands.ClientesCommands.AlterarEnderecoDoCliente;

public sealed class AlterarEnderecoDoClienteCommand
{
    public Guid ClienteId { get; }
    public Guid EnderecoId { get; }
    public string Cep { get; }
    public string Logradouro { get; }
    public string Numero { get; }
    public string Bairro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public string Pais { get; }
    public string Complemento { get; }

    public AlterarEnderecoDoClienteCommand(Guid clienteId, Guid enderecoId, string cep,
        string logradouro, string numero, string bairro, string cidade, string estado, string pais,
        string complemento)
    {
        ClienteId = clienteId; EnderecoId = enderecoId; Cep = cep; Logradouro = logradouro;
        Numero = numero; Bairro = bairro; Cidade = cidade; Estado = estado; Pais = pais;
        Complemento = complemento;
    }
}
