using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using N8T.Domain;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace N8T.Infrastructure.Data
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDomainEventContext _domainEventContext;
        private readonly IDbFacadeResolver _dbFacadeResolver;
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IDbFacadeResolver dbFacadeResolver,
            IDomainEventContext domainEventContext,
            IMediator mediator,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _domainEventContext = domainEventContext ?? throw new ArgumentNullException(nameof(domainEventContext));
            _dbFacadeResolver = dbFacadeResolver ?? throw new ArgumentNullException(nameof(dbFacadeResolver));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (next.Method.DeclaringType != null)
            {
                var argumentTypes = next.Method.DeclaringType.GenericTypeArguments;
                var transactionAttr = argumentTypes[0].GetCustomAttributes(typeof(TransactionScopeAttribute), true);
                if (transactionAttr.Length < 1)
                {
                    return await next();
                }
            }

            _logger.LogInformation($"Open the transaction for {nameof(request)}.");
            var strategy = _dbFacadeResolver.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                // Achieving atomicity
                await using var transaction = _dbFacadeResolver.Database.BeginTransaction(IsolationLevel.ReadCommitted);

                _logger.LogInformation($"Execute the {nameof(request)} request.");
                var response = await next();

                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation($"Publish domain events for {request}.");
                var domainEvents = _domainEventContext.GetDomainEvents().ToList();

                var tasks = domainEvents
                    .Select(async @event => { await _mediator.Publish(@event, cancellationToken); });

                await Task.WhenAll(tasks);

                _logger.LogInformation($"Finish task for {request}.");

                return response;
            });
        }
    }
}