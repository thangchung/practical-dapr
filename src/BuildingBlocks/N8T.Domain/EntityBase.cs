using System;
using System.Collections.Generic;

namespace N8T.Domain
{
    public abstract class EntityBase
    {
        public DateTime Created { get; protected set; }
        public DateTime? Updated { get; protected set; }
        public HashSet<IDomainEvent> DomainEvents { get; private set; }

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents ??= new HashSet<IDomainEvent>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }
}