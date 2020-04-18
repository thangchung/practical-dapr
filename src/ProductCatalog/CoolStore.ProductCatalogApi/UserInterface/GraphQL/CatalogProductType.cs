using System;
using CoolStore.ProductCatalogApi.Infrastructure.Gateways;
using CoolStore.Protobuf.Inventory.V1;
using CoolStore.Protobuf.ProductCatalog.V1;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using N8T.Infrastructure;
using N8T.Infrastructure.GraphQL;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class CatalogProductType : ProtoObjectType<CatalogProductDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CatalogProductDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();
            descriptor.Field(t => t.CategoryId).Type<NonNullType<UuidType>>();

            descriptor.Field("inventory").Type<NonNullType<InventoryType>>().Resolver(async ctx =>
            {
                var inventoryGateway = ctx.Service<IInventoryGateway>();

                var dataLoader = ctx.BatchDataLoader<Guid, InventoryDto>(
                    "InventoryById",
                    inventoryGateway.GetInventoriesAsync);

                return await dataLoader.LoadAsync(ctx.Parent<CatalogProductDto>().InventoryId.ConvertTo<Guid>());
            });

            // ignore fields
            base.Configure(descriptor);
            descriptor.Ignore(t => t.InventoryId);
        }
    }
}
