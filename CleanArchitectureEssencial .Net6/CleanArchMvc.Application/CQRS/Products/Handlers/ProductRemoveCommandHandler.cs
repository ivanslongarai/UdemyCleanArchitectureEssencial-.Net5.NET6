using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;

        public ProductRemoveCommandHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<GenericCommandResult> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.Valid)
                return new GenericCommandResult(false, "Removing product error", null, request.Notifications);

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return new GenericCommandResult(false, "Product id could not be found", null, new { Id = request.Id });
            else
            {
                var result = await _productRepository.RemoveAsync(product);
                return new GenericCommandResult(true, "Removing product succeffully", product, null);
            }
        }
    }
}