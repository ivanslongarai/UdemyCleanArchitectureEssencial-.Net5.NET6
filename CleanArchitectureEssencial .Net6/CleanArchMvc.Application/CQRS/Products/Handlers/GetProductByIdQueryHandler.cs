using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Queries;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<GenericCommandResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Valid)
            {
                var result = await _productRepository.GetByIdAsync(request.Id);
                return new GenericCommandResult(true, "Getting product by id successfully", result, null);
            }
            return new GenericCommandResult(false, "Getting product by id error", null, request.Notifications);
        }
    }
}