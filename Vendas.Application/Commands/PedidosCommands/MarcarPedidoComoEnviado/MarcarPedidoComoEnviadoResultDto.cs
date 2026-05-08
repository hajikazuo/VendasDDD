using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado
{
    public sealed class MarcarPedidoComoEnviadoResultDto
    {
        public Guid PedidoId { get; init; }
        public string StatusPedido { get; init; } = string.Empty;
    }
}
