using HotChocolate.Types;

namespace CoolStore.ShoppingCartApi.Infrastructure.Apis.GraphQL
{
    public sealed class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
        }
    }
}
