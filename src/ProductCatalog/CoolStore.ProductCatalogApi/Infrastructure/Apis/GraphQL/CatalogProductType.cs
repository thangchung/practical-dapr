using System;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Dtos;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using N8T.Infrastructure;

namespace CoolStore.ProductCatalogApi.Apis.GraphQL
{
    public class CatalogProductType : ObjectType<CatalogProductDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CatalogProductDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            descriptor.Field(t => t.Category).Type<NonNullType<CategoryType>>();

            descriptor.Field("store").Type<NonNullType<InventoryType>>().Resolver(async ctx =>
            {
                var inventoryGateway = ctx.Service<IStoreGateway>();

                var dataLoader = ctx.BatchDataLoader<Guid, Protobuf.Inventory.V1.StoreDto>(
                    "StoreById",
                    inventoryGateway.GetStoresAsync);

                return await dataLoader.LoadAsync(ctx.Parent<CatalogProductDto>().StoreId.ConvertTo<Guid>());
            });

            // ignore fields
            base.Configure(descriptor);
            descriptor.Ignore(t => t.StoreId);
        }
    }
}
