using System;
using System.Collections.Generic;
using System.Text;
using Vendas.Domain.Pedidos.Entities;

namespace Vendas.Application.Abstractions.Persistence
{
    public interface IPedidoRepository
    {
        Task<Pedido?> ObterPorIdAsync(Guid pedidoId, CancellationToken cancellationToken = default);

        Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default);

        Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default);
    }
}
