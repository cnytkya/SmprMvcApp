using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SmprMvcApp.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmprMvcApp.EntityLayer.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [DisplayName("List Price"), Required, Range(1, 1000)]
        public double ListPrice { get; set; }

        [DisplayName("Price for 1-50"), Required, Range(1, 1000)]
        public double Price { get; set; }

        [DisplayName("Price for 50+"), Required, Range(1, 1000)]
        public double Price50 { get; set; }

        [DisplayName("Price for 100++"), Required, Range(1, 1000)]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId"), ValidateNever]
        public Category Category { get; set; }//Ürün İlişkisi İçin Yabancı Anahtar Eklendi

        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}