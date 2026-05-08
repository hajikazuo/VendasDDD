using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Application.Commands.PedidosCommands.CancelarPedido
{
    public sealed class CancelarPedidoResultDto
    {
        public Guid PedidoId { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
