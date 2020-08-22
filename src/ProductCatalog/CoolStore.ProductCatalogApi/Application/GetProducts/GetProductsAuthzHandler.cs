using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoolStore.ProductCatalogApi.Application.GetProducts
{
    public class GetProductsAuthzHandler : AuthorizationHandler<GetProductsQuery>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetProductsQuery requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
