using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Queries;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<GenericCommandResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetProductsAsync();
            return new GenericCommandResult(true, "Getting products successfully", result, null);
        }
    }
}