using FluentValidation;

namespace CoolStore.InventoryApi.Application.UseCases.GetInventory
{
    public class GetInventoriesByIdsValidator : AbstractValidator<GetInventoriesByIdsQuery>
    {
        public GetInventoriesByIdsValidator()
        {
        }
    }
}
