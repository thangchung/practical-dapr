using System;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Dtos;
using GreenDonut;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using N8T.Infrastructure;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class CatalogProductType : ObjectType<CatalogProductDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CatalogProductDto> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();

            descriptor.Field(t => t.Category).Type<NonNullType<CategoryType>>();

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
