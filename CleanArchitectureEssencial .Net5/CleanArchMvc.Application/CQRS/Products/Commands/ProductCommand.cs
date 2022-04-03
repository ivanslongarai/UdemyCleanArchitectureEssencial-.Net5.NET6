using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.CQRS.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CleanArchMvc.Application.CQRS.Products.Commands
{
    public abstract class ProductCommand : Notifiable, IRequest<GenericCommandResult>, ICommand
    {
        protected ProductCommand(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CategoryId = categoryId;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        // Fail Fast Validation

        public virtual bool Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                // Name
                .IsNotNullOrEmpty(Name, "ProductCommand.Name", "Name is required")
                .IsFalse(string.IsNullOrEmpty(Name) == false && Name?.Length < 3, "Product.Name", "Invalid Name, too short, minimum 3 characteres")
                .IsFalse(string.IsNullOrEmpty(Name) == false && Name?.Length > 80, "Product.Name", "Invalid Name, too long, maximum 80 characteres")
                //Description
                .IsNotNullOrEmpty(Description, "ProductCommand.Description", "Description is required")
                .IsFalse(string.IsNullOrEmpty(Description) == false && Description?.Length < 3, "Product.Description", "Invalid Description, too short, minimum 3 characteres")
                .IsFalse(string.IsNullOrEmpty(Description) == false && Description?.Length > 80, "Product.Description", "Invalid Description, too long, maximum 80 characteres")
                //Price and Stock
                .IsGreaterOrEqualsThan(Price, 0, "ProductCommand.Price", "Invalid Price value")
                .IsGreaterOrEqualsThan(Stock, 0, "ProductCommand.Stock", "Invalid Stock value")
                //Image
                .IsFalse(string.IsNullOrEmpty(Image) == false && Image?.Length < 3, "ProductCommand.Image", "Invalid Image, too short, minimum 3 characteres")
                .IsFalse(string.IsNullOrEmpty(Image) == false && Image?.Length > 250, "ProductCommand.Image", "Invalid Image, too long, maximum 250 characteres")
                //CategoryId
                .IsTrue(CategoryId > 0, "ProductCommand.CategoryId", "Invalid category id"));
            return Valid;
        }
    }
}