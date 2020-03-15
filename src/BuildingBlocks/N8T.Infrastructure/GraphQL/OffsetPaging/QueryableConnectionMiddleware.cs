using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Resolvers;

namespace N8T.Infrastructure.GraphQL.OffsetPaging
{
    public class QueryableConnectionMiddleware<TValueType> where TValueType : new()
    {
        private readonly FieldDelegate _next;

        public QueryableConnectionMiddleware(FieldDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            await _next(context).ConfigureAwait(false);

            var page = context.Argument<int?>("page") ?? 1;
            if (page <= 0)
            {
                page = 1;
            }

            var pageSize = context.Argument<int?>("pageSize") ?? 10;

            var source = context.Result switch
            {
                IQueryable<TValueType> q => q,
                IEnumerable<TValueType> e => e.AsQueryable(),
                _ => null
            };

            if (source != null)
            {
                context.Result = new OffsetPaging<TValueType>
                {
                    TotalCount = source.LongCount(),
                    Edges = source
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList()
                };
            }
        }
    }
}