﻿using Microsoft.EntityFrameworkCore;
using N8T.Domain;
using System.Collections.Generic;
using System.Linq;

namespace N8T.Infrastructure.Data
{
    public abstract class AppDbContextBase : DbContext, IDomainEventContext, IDbFacadeResolver
    {
        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public IEnumerable<DomainEventBase> GetDomainEvents()
        {
            var domainEntities = ChangeTracker
                .Entries<EntityBase>()
                .Where(x =>
                    x.Entity.DomainEvents != null &&
                    x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

            return domainEvents;
        }
    }
}