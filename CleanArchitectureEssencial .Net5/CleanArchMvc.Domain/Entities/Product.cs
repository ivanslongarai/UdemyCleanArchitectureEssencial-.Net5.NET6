using System;
using Flunt.Notifications;
using Flunt.Validations;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CreatedAt = DateTime.Now;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }


        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CategoryId = categoryId;
        }

        public Product AddCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
            return this;
        }

        public Product SetCategoryId(int id)
        {
            CategoryId = id;
            return this;
        }

        public override void Validate()
        {
            if (Category != null)
                Category.Validate();

            AddNotifications(new Contract()
                    .Requires()
                    // Id
                    .IsGreaterThan(Id, 0, "Product.Id", "Invalid Id")
                    // Name
                    .IsNotNullOrEmpty(Name, "Product.Name", "Name is required")
                    .IsFalse(string.IsNullOrEmpty(Name) == false && Name?.Length < 3, "Product.Name", "Invalid Name, too short, minimum 3 characteres")
                    .IsFalse(string.IsNullOrEmpty(Name) == false && Name?.Length > 80, "Product.Name", "Invalid Name, too long, maximum 80 characteres")
                    //Description
                    .IsNotNullOrEmpty(Description, "Product.Description", "Description is required")
                    .IsFalse(string.IsNullOrEmpty(Description) == false && Description?.Length < 3, "Product.Description", "Invalid Description, too short, minimum 3 characteres")
                    .IsFalse(string.IsNullOrEmpty(Description) == false && Description?.Length > 80, "Product.Description", "Invalid Description, too long, maximum 80 characteres")
                    //Price and Stock
                    .IsGreaterOrEqualsThan(Price, 0, "Product.Price", "Invalid Price value")
                    .IsGreaterOrEqualsThan(Stock, 0, "Product.Stock", "Invalid Stock value")
                    //Image
                    .IsFalse(string.IsNullOrEmpty(Image) == false && Image?.Length < 3, "Product.Image", "Invalid Image, too short, minimum 3 characteres")
                    .IsFalse(string.IsNullOrEmpty(Image) == false && Image?.Length > 250, "Product.Image", "Invalid Image, too long, maximum 250 characteres")
                    //Category
                    .IsTrue(Category != null, "Product.Category", "Invalid Category")
                );

            if (Category != null)
                if (Category.Valid == false)
                    AddNotifications(Category);

        }
    }
}