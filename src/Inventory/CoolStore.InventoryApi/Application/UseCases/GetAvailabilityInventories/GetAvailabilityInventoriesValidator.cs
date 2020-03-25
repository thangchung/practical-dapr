using FluentValidation;

namespace CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories
{
    public class GetAvailabilityInventoriesValidator : AbstractValidator<GetInventoriesQuery>
    {
        public GetAvailabilityInventoriesValidator()
        {
        }
    }
}
