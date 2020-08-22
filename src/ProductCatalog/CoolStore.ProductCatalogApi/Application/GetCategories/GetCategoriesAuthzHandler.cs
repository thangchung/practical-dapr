using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoolStore.ProductCatalogApi.Application.GetCategories
{
    public class GetCategoriesAuthzHandler : AuthorizationHandler<GetCategoriesQuery>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GetCategoriesQuery requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
