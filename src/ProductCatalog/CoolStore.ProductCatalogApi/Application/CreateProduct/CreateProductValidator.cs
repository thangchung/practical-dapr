using FluentValidation;

namespace CoolStore.ProductCatalogApi.Application.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("${Name} couldn't be null or empty.");

            RuleFor(x => x.ImageUrl)
                .NotNull()
                .NotEmpty()
                .WithMessage("${ImageUrl} couldn't be null or empty.");

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("${HighPrice} couldn't be null, empty and less than zero.");

            RuleFor(x => x.CategoryId)
                .NotNull()
                .NotEmpty()
                .WithMessage("${CategoryId} couldn't be null or empty.");

            RuleFor(x => x.StoreId)
                .NotNull()
                .NotEmpty()
                .WithMessage("${StoreId} couldn't be null or empty.");
        }
    }
}
