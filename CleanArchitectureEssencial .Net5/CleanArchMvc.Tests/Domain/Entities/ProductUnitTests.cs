using CleanArchMvc.Domain.Entities;
using Xunit;
using FluentAssertions;
using System;

namespace CleanArchMvc.Tests.Domain.Entities
{

    public class ProductUnitTests
    {

        [Fact(DisplayName = "Create Product Object with Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            var product = new Product(
                    1, "Product Name", "Product Description"
                    , 10, 10, "Image Field"
                );
            var category = new Category(1, "Category Name");
            product.AddCategory(category);
            product.Validate();
            Assert.True(product.Valid);
        }

        [Fact(DisplayName = "Create Product Object without a Valid Category State")]
        public void CreateProduct_WithoutValidCategoryState_ResultObjectInvalidState()
        {
            var product = new Product(
                    1, "Product Name", "Product Description"
                    , 10, 10, "Image Field"
                );
            var category = new Category(1, "");
            product.AddCategory(category);
            product.Validate();
            Assert.False(product.Valid);
        }

        [Fact(DisplayName = "Create Product Object with invalid Params 1")]
        public void CreateProduct_WithInvalidParams_ResultObjectInvalidState_1()
        {
            var product = new Product(
                    -1, "", ""
                    , -1, -1, "@"
                );
            product.Validate();
            Assert.True(product.Notifications.Count == 7);

            /*
                Count = 7
                [0]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Id"
                Property [string]:"Product.Id"
                [1]:{Flunt.Notifications.Notification}
                Message [string]:"Name is required"
                Property [string]:"Product.Name"
                [2]:{Flunt.Notifications.Notification}
                Message [string]:"Description is required"
                Property [string]:"Product.Description"
                [3]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Price value"
                Property [string]:"Product.Price"
                [4]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Stock value"
                Property [string]:"Product.Stock"
                [5]:{Flunt.Notifications.Notification}               
                Message [string]:"Invalid Image, too short, minimum 3 characteres"
                Property [string]:"Product.Image"
                [6]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Category"
                Property [string]:"Product.Category"
            */

        }

        [Fact(DisplayName = "Create Product Object with invalid Params 2")]
        public void CreateProduct_WithInvalidParams_ResultObjectInvalidState_2()
        {
            var product = new Product(
                    -5, "a", "b"
                    , -1, -1, "@"
                );

            var category = new Category(1, "a");
            product.AddCategory(category);

            product.Validate();
            Assert.True(product.Notifications.Count == 7);

            /*
                [0]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Id"
                Property [string]:"Product.Id"
                [1]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Name, too short, minimum 3 characteres"
                Property [string]:"Product.Name"
                [2]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Description, too short, minimum 3 characteres"
                Property [string]:"Product.Description"
                [3]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Price value"
                Property [string]:"Product.Price"
                [4]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Stock value"
                Property [string]:"Product.Stock"
                [5]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid Image, too short, minimum 3 characteres"
                Property [string]:"Product.Image"
                [6]:{Flunt.Notifications.Notification}
                Message [string]:"Invalid name, too short, minimum 3 characteres"
                Property [string]:"Category.Name"            
            */
        }

        [Fact(DisplayName = "Create Product Object with invalid Params 3")]
        public void CreateProduct_WithInvalidParams_NotExceptions()
        {
            Action action = () => new Product(
                    -5, "a", "b"
                    , -1, -1, "@"
                ).AddCategory(null).Validate();
            action.Should().NotThrow<Exception>();
        }
    }
}