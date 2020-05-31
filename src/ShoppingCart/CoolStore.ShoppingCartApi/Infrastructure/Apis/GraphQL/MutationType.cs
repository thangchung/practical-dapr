using HotChocolate.Types;

namespace CoolStore.ShoppingCartApi.Infrastructure.Apis.GraphQL
{
    public sealed class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
        }
    }
}
