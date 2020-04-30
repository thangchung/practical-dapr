using FluentValidation;

namespace CoolStore.InventoryApi.Application.UseCases.GetStoresByIds
{
    public class GetStoresByIdsValidator
        : AbstractValidator<GetStoresByIdsQuery>
    {
        public GetStoresByIdsValidator()
        {
        }
    }
}
