using Vendas.Domain.Pedidos.Integration.Clientes;

namespace Vendas.Infra.Fakes
{
    public sealed class FakeClientesGateway : IClientesGateway
    {
        private static readonly Dictionary<Guid, Dictionary<Guid, EnderecoDto>> _clientes = new()
        {
            [new Guid("22222222-0000-0000-0000-000000000001")] = new()
            {
                [new Guid("33333333-0000-0000-0000-000000000001")] = 
                new(
                    id: new Guid("33333333-0000-0000-0000-000000000001"),
                    cep: "01310-100",
                    logradouro: "Avenida Paulista",
                    numero: "1578",
                    bairro: "Bela Vista",
                    cidade: "São Paulo",
                    estado: "SP",
                    pais: "Brasil",
                    complemento: "Conj 42"
                ),
                [new Guid("33333333-0000-0000-0000-000000000002")] =
                new(
                    id: new Guid("33333333-0000-0000-0000-000000000002"),
                    cep: "04578-000",
                    logradouro: "Vila Olímpia",
                    numero: "300",
                    bairro: "Bela Vista",
                    cidade: "São Paulo",
                    estado: "SP",
                    pais: "Brasil",
                    complemento: ""
                ),
            },
            [new Guid("22222222-0000-0000-0000-000000000002")] = new()
            {
                [new Guid("33333333-0000-0000-0000-000000000003")] =
                new(
                    id: new Guid("33333333-0000-0000-0000-000000000003"),
                    cep: "30130-110",
                    logradouro: "Avenida do Contorno",
                    numero: "8000",
                    bairro: "Santo Agostinho",
                    cidade: "Belo Horizonte",
                    estado: "MG",
                    pais: "Brasil",
                    complemento: "Bloco B"
                )
            },
        };

        public Task<EnderecoDto?> ObterEnderecoAsync(Guid clienteId, Guid enderecoId, CancellationToken cancellationToken = default)
        {
            if(_clientes.TryGetValue(clienteId, out var enderecos) && enderecos.TryGetValue(enderecoId, out var endereco))
            {
                return Task.FromResult<EnderecoDto?>(endereco);
            }

            return Task.FromResult<EnderecoDto?>(null);
        }
    }
}
