using HotChocolate.Types;

namespace CoolStore.InventoryApi.Infrastructure.Apis.GraphQL
{
    public sealed class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor
                .Field(x => x.GetStores(default!))
                .Name("stores");
        }
    }
}
