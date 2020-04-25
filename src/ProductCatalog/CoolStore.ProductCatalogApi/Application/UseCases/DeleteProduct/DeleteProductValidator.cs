using FluentValidation;

namespace CoolStore.ProductCatalogApi.Application.UseCases.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("${Id} couldn't be null or empty.");
        }
    }
}
