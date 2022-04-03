using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Application.CQRS.Queries;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services
{

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<GenericCommandResult> CreateAsync(ProductDTO productDTO)
        {

            var product = _mapper.Map<ProductCreateCommand>(productDTO);
            var result = await _mediator.Send(product);

            if (result.Success)
            {
                var resultDto = _mapper.Map<ProductDTO>(result.Data);
                return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    resultDto,
                    null);
            }

            return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    null,
                    result.Notifications);

        }

        public async Task<GenericCommandResult> GetByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            if (result.Success)
            {
                var resultDto = _mapper.Map<ProductDTO>(result.Data);
                return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    resultDto,
                    null);
            }

            return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    null,
                    result.Notifications);
        }

        public async Task<GenericCommandResult> GetProductsAsync()
        {

            var result = await _mediator.Send(new GetProductsQuery());

            if (result.Success)
            {
                var resultDto = _mapper.Map<IEnumerable<ProductDTO>>(result.Data);
                return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    resultDto,
                    null);
            }

            return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    null,
                    result.Notifications);
        }

        public async Task<GenericCommandResult> RemoveAsync(int id)
        {
            var result = await _mediator.Send(new ProductRemoveCommand(id));

            if (result.Success)
            {
                var resultDto = _mapper.Map<ProductDTO>(result.Data);
                return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    resultDto,
                    null);
            }

            return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    null,
                    result.Notifications);
        }

        public async Task<GenericCommandResult> UpdateAsync(ProductDTO productDTO)
        {

            var product = _mapper.Map<ProductUpdateCommand>(productDTO);
            var result = await _mediator.Send(product);

            if (result.Success)
            {
                var resultDto = _mapper.Map<ProductDTO>(result.Data);
                return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    resultDto,
                    null);
            }

            return new GenericCommandResult(
                    result.Success,
                    result.Message,
                    null,
                    result.Notifications);
        }
    }
}
