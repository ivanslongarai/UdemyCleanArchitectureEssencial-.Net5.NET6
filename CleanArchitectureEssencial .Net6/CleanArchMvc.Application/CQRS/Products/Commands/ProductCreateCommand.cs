
namespace CleanArchMvc.Application.CQRS.Products.Commands
{
    public class ProductCreateCommand : ProductCommand
    {

        public ProductCreateCommand(string name, string description, decimal price, int stock, string image, int categoryId) :
         base(name, description, price, stock, image, categoryId)
        {

        }

    }
}