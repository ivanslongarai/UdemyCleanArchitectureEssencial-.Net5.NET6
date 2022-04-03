using CleanArchMvc.Application.CQRS.CommandResults;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Queries
{
    public class GetProductsQuery : IRequest<GenericCommandResult> { }
}
