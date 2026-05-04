using System;
using System.Collections.Generic;
using System.Text;
using Vendas.Domain.Common.Interfaces;

namespace Vendas.Domain.Common.Base
{
    public abstract record class DomainEventBase : IDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
