using FluentValidation;

namespace CoolStore.InventoryApi.UseCases.GetAvailabilityInventories
{
    public class GetAvailabilityInventoriesValidator : AbstractValidator<GetInventoriesQuery>
    {
        public GetAvailabilityInventoriesValidator()
        {
        }
    }
}
