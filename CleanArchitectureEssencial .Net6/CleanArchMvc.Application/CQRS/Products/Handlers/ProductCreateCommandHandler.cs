using System.Threading;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<GenericCommandResult> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.Valid)
                return new GenericCommandResult(false, "Creating product error", null, request.Notifications);

            var product = new Product(
                    request.Name,
                    request.Description,
                    request.Price,
                    request.Stock,
                    request.Image);

            product.SetCategoryId(request.CategoryId);
            var result = await _productRepository.CreateAsync(product);
            return new GenericCommandResult(true, "Creating product successfully", result, null);
        }
    }
}