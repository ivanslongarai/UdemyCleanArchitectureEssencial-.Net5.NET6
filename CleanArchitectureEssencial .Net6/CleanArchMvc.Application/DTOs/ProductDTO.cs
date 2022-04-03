using System.Data.Common;
using System;
using System.ComponentModel.DataAnnotations;
using CleanArchMvc.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(5)]
        [MaxLength(80)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        [Range(typeof(decimal), "1", "79228162514264337593543950335", ErrorMessage ="Invalid price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(1, 9999, ErrorMessage ="Stock has to be between 1 and 9999")]
        [Column(TypeName = "decimal(18,1)")]
        [DisplayFormat(DataFormatString = "{0:C1}")]
        [DisplayName("Stock")]
        public int Stock { get; set; }

        [MaxLength(250)]
        [DisplayName("Product Image")]
        public string Image { get; set; }

        [DisplayName("Categories")]
        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

    }
}