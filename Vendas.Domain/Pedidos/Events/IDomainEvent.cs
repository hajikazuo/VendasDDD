using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Domain.Pedidos.Events
{
    public interface IDomainEvent
    {
        DateTime DateOccurred { get; }
    }
}
