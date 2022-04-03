using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;

        public ProductUpdateCommandHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<GenericCommandResult> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.Valid)
                return new GenericCommandResult(false, "Updating product error", null, request.Notifications);

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
                return new GenericCommandResult(false, "Product id could not be found", null, new { Id = request.Id });
            else
            {
                product.Update(
                    request.Name,
                    request.Description,
                    request.Price,
                    request.Stock,
                    request.Image,
                    request.CategoryId
                );
                var result = await _productRepository.UpdateAsync(product);
                return new GenericCommandResult(true, "Updating product succeffully", result, null);
            }
        }
    }
}