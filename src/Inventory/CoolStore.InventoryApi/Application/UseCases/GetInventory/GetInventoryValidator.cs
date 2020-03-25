using FluentValidation;

namespace CoolStore.InventoryApi.Application.UseCases.GetInventory
{
    public class GetInventoryValidator : AbstractValidator<GetInventoryQuery>
    {
        public GetInventoryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("${InventoryId} couldn't be null or empty.");
        }
    }
}
