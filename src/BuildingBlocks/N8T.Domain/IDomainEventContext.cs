using System.Collections.Generic;

namespace N8T.Domain
{
    public interface IDomainEventContext
    {
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}