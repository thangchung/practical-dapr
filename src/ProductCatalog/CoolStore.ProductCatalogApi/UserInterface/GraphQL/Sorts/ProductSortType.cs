using CoolStore.ProductCatalogApi.Dtos;
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
