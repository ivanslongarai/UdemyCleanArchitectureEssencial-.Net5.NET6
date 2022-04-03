using System;
using System.Collections.Generic;
using Flunt.Validations;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        private readonly List<Product> _products = new List<Product>();

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }

        public Category(string name)
        {
            Name = name;
            CreatedAt = DateTime.Now;
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<Product> Products { get => _products.ToArray(); }

        // Fail Fast Validation

        public override void Validate()
        {
            AddNotifications(new Contract()
                    .Requires()
                    //Id
                    .IsGreaterThan(Id, 0, "Category.Id", "Invalid Id")
                    //Name
                    .IsNotNullOrEmpty(Name, "Category.Name", "Name is required")
                    .HasMinLen(Name, 3, "Category.Name", "Invalid name, too short, minimum 3 characteres")
                    .HasMaxLen(Name, 50, "Category.Name", "Invalid name, too long, maximum 50 characteres")
                );
        }

        public Category Change(string name)
        {
            Name = name;
            return this;
        }
    }
}