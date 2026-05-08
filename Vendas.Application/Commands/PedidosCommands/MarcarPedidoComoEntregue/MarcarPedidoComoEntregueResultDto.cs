using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue
{
    public sealed class MarcarPedidoComoEntregueResultDto
    {
        public Guid PedidoId { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
