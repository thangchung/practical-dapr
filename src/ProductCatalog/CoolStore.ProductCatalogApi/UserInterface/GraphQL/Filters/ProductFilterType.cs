using CoolStore.ProductCatalogApi.Dtos;
using HotChocolate.Types.Filters;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL.Filters
{
    public class ProductFilterType : FilterInputType<CatalogProductDto>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CatalogProductDto> descriptor)
        {
            descriptor
                .BindFieldsExplicitly()
                .Filter(t => t.Name)
                .BindFiltersExplicitly()
                .AllowEquals().And()
                .AllowContains();

            descriptor
                .BindFieldsExplicitly()
                .Filter(t => t.Price)
                .BindFiltersExplicitly()
                .AllowEquals().And()
                .AllowGreaterThanOrEquals().And()
                .AllowLowerThanOrEquals();
        }
    }
}
