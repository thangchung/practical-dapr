using System;
using MediatR;

namespace N8T.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
    }
}