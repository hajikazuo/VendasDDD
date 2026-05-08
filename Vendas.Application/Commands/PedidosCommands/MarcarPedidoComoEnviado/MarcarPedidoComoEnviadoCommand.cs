using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado
{
    public sealed class MarcarPedidoComoEnviadoCommand
    {
        public Guid PedidoId { get; }

        public MarcarPedidoComoEnviadoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}
