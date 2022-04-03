using CleanArchMvc.Application.CQRS.CommandResults;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Commands
{
    public class ProductRemoveCommand : Notifiable, IRequest<GenericCommandResult>
    {
        public ProductRemoveCommand(int id) => Id = id;

        public int Id { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Id > 0, "ProductRemoveCommand.Id", "Invalid id"));
            return Valid;
        }
    }
}