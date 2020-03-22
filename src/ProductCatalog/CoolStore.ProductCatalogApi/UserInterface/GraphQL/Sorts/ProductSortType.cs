using CoolStore.Protobuf.ProductCatalog.V1;
using HotChocolate.Types.Sorting;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL.Sorts
{
    public class ProductSortType : SortInputType<CatalogProductDto>
    {
        protected override void Configure(ISortInputTypeDescriptor<CatalogProductDto> descriptor)
        {
            descriptor
                .BindFieldsExplicitly()
                .Sortable(t => t.Name);

            descriptor
                .BindFieldsExplicitly()
                .Sortable(t => t.Price);
        }
    }
}