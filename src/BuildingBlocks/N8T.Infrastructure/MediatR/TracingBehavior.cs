using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace N8T.Infrastructure.MediatR
{
    public class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var activity = new Activity($"Handling the RequestHandler<{typeof(TRequest).Name}, {typeof(TResponse).Name}>")
                .AddBaggage($"{typeof(TRequest).Name}", JsonSerializer.Serialize(request))
                .Start();

            try
            {
                return await next();
            }
            finally
            {
                activity.Stop();
            }
        }
    }
}
