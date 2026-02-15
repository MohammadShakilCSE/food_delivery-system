using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}
