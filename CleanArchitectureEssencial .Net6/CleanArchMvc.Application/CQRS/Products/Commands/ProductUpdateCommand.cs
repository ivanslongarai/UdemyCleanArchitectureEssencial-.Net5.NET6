using Flunt.Validations;

namespace CleanArchMvc.Application.CQRS.Products.Commands
{
    public class ProductUpdateCommand : ProductCommand
    {
        public int Id { get; set; }

        public ProductUpdateCommand(string name, string description, decimal price, int stock, string image, int categoryId, int id) :
         base(name, description, price, stock, image, categoryId)
        {
            Id = id;
        }

        public override bool Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Id > 0, "ProductUpdateCommand.Id", "Invalid id"));
            return base.Validate() && Valid;
        }
    }
}