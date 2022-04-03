using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Interfaces;
using Flunt.Validations;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Queries
{
    public class GetProductByIdQuery : ProductQuery, IRequest<GenericCommandResult>, IQuery
    {
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Id > 0, "GetProductByIdQuery.Id", "Invalid id"));
            return Valid;
        }
    }
}
