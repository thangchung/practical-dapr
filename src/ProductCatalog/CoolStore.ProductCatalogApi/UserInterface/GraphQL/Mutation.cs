using System.Threading.Tasks;
using CoolStore.Protobuf.ProductCatalog.V1;
using MediatR;

namespace CoolStore.ProductCatalogApi.UserInterface.GraphQL
{
    public class Mutation
    {
        private readonly IMediator _mediator;

        public Mutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductInput)
        {
            return await _mediator.Send(createProductInput);
        }
    }
}
