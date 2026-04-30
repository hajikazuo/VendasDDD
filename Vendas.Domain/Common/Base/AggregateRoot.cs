using System;
using System.Collections.Generic;
using System.Text;

namespace Vendas.Domain.Common.Base
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot() : base()
        { 
        }

        protected AggregateRoot(Guid id) : base(id)
        {
        }
    }
}
