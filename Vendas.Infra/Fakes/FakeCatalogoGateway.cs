using Vendas.Domain.Pedidos.Integration.Catalogo;

namespace Vendas.Infra.Fakes
{
    public sealed class FakeCatalogoGateway : ICatalogoGateway
    {
        private static readonly Dictionary<Guid, ProdutoDto> _produtos = new()
        {
            [new Guid("11111111-0000-0000-0000-000000000001")] = new(new Guid("11111111-0000-0000-0000-000000000001"), "Notebook Gamer RTX 4060", 8500.00m),
            [new Guid("11111111-0000-0000-0000-000000000002")] = new(new Guid("11111111-0000-0000-0000-000000000002"), "Mouse Sem Fio Logitech MX Master", 450.00m),
            [new Guid("11111111-0000-0000-0000-000000000003")] = new(new Guid("11111111-0000-0000-0000-000000000003"), "Teclado Mecânico Keychron K8", 680.00m),
            [new Guid("11111111-0000-0000-0000-000000000004")] = new(new Guid("11111111-0000-0000-0000-000000000004"), "Monitor Ultrawide 34 polegadas", 3200.00m)
        };

        public Task<ProdutoDto?> ObterProdutoPorIdAsync(Guid produtoId, CancellationToken cancellationToken = default)
        {
            _produtos.TryGetValue(produtoId, out var produto);
            return Task.FromResult(produto);
        }
    }
}
