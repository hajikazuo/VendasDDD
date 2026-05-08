using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue
{
    public sealed class MarcarPedidoComoEntregueCommand
    {
        public Guid PedidoId { get; }

        public MarcarPedidoComoEntregueCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
