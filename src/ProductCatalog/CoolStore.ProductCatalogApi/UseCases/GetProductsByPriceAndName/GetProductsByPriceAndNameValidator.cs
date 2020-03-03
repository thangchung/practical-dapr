using CoolStore.Protobuf.ProductCatalog.V1;
using FluentValidation;

namespace CoolStore.ProductCatalogApi.UseCases.GetProductsByPriceAndName
{
    public class GetProductsByPriceAndNameValidator : AbstractValidator<GetProductsRequest>
    {
        public GetProductsByPriceAndNameValidator()
        {
            RuleFor(x => x.HighPrice)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("${HighPrice} could not be null, empty and less than zero.");
        }
    }
}