using System.Linq;
using CoolStore.InventoryApi.Dtos;
using MediatR;

namespace CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories
{
    public class GetInventoriesQuery : IRequest<IQueryable<InventoryDto>>
    {
    }
}
