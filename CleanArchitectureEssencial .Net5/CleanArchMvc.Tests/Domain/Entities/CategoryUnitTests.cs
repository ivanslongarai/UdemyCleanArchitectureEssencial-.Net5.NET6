using Xunit;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;

namespace CleanArchMvc.Tests.Domain.Entities
{

    public class CategoryUnitTests
    {
        [Fact(DisplayName = "Create Category Object with Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            var category = new Category(1, "Category Name");
            category.Validate();
            Assert.True(category.Valid);
        }

        [Fact(DisplayName = "Create Category Object with Invalid Id")]
        public void CreateCategory_WithInvalidId_ResultObjectInvalidState()
        {
            var category = new Category(-1, "Category Name");
            category.Validate();
            Assert.False(category.Valid);
        }

        [Fact(DisplayName = "Create Category Object with Invalid Name")]
        public void CreateCategory_WithInvalidName_ResultObjectInvalidState()
        {
            var category = new Category(1, "");
            category.Validate();
            Assert.False(category.Valid);
        }

        [Fact(DisplayName = "Changing CategoryName to an Invalid name")]
        public void CreateAndChangeCategoryName_WithInvalidName_ResultObjectInvalidState()
        {
            var category = new Category(1, "Ivan");
            category.Validate();
            category.Change("Iv");
            category.Validate();
            Assert.False(category.Notifications.Count == 0);
        }

        [Fact(DisplayName = "Create Valid Category with no execeptions")]
        public void CreateValidCategory_WithValidParameters_ResultNoExceptions()
        {
            Action action = () => new Category(1, "Ivan");
            action.Should().NotThrow<Exception>();
        }

        [Fact(DisplayName = "Create Invalid Category with no execeptions")]
        public void CreateInvalidCategory_WithValidParameters_ResultNoExceptions()
        {
            Action action = () => new Category(1, null);
            action.Should().NotThrow<Exception>();
        }

    }
}