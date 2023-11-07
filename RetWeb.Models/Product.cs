using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cartopia.Models
{
    public class Product
    {

        /// <summary>
        /// Id of the Product
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the Product
        /// </summary>
        [Required]
        [DisplayName("Product Name")]
        public string? Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string ISBN { get; set; }

        /// <summary>
        ///  Author
        /// </summary>
        [Required]
        public string Author { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        /// <summary>
        /// Price of the product 1-50
        /// </summary>
        [Required]
        [Display(Name = "Price for Less than 50 Copies")]
        [Range(1, 1000)]
        public double Price { get; set; }

        /// <summary>
        /// Price of the product > 50
        /// </summary>
        [Required]
        [Display(Name = "Price for 50+ Copies")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        /// <summary>
        /// Price of the product > 100
        /// </summary>
        [Required]
        [Display(Name = "Price for 100+ Copies")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryId { get; set; } //we need to tell the table that this is a foreign key explicitly adding a navigation property to the category table on the nxt line
        [ForeignKey("CategoryId")] // this helps us to indicate that this category table has a foreign key which is categoryId
        [ValidateNever]
        public Category Category { get; set; }

        /// <summary>
        /// product Image
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Deleted flag
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
