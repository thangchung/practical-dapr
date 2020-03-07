using CoolStore.Protobuf.ProductCatalog.V1;
using FluentValidation;

namespace CoolStore.ProductCatalogApi.UseCases.GetProductsByPriceAndName
{
    public class GetProductsValidator : AbstractValidator<GetProductsRequest>
    {
    }
}