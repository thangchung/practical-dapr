using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using N8T.Domain;
using System.Linq;

namespace N8T.Infrastructure.Data
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventContext _domainEventContext;
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IUnitOfWork unitOfWork,
            IDomainEventContext domainEventContext,
            IMediator mediator,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _domainEventContext = domainEventContext ?? throw new ArgumentNullException(nameof(domainEventContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _logger.LogInformation($"Open the transaction for {nameof(request)}.");
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                _logger.LogInformation($"Execute the {nameof(request)} request.");
                var response = await next();

                _logger.LogInformation($"Commit the transaction for {nameof(request)}.");
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                _logger.LogInformation($"Publish domain events for {request}.");
                var domainEvents = _domainEventContext.GetDomainEvents().ToList();

                var tasks = domainEvents
                    .Select(async @event => { await _mediator.Publish(@event, cancellationToken); });

                await Task.WhenAll(tasks);

                _logger.LogInformation($"Finish task for {request}.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Got the error {ex.Message}.");
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}