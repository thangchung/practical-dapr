using FluentValidation;

namespace CoolStore.InventoryApi.Application.UseCases.GetAvailabilityStores
{
    public class GetAvailabilityStoresValidator
        : AbstractValidator<GetStoresQuery>
    {
        public GetAvailabilityStoresValidator()
        {
        }
    }
}
