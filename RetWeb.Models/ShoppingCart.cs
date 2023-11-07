using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Cartopia.Models.Models;

namespace Cartopia.Models
{
    public class ShoppingCart
    {

        /// <summary>
        /// Id of the Product
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The quantity
        /// </summary>
        [Range(1, 1000, ErrorMessage ="Please enter a value between 1 and 1000" )]
        public int Count { get; set; }


        /// <summary>
        /// The Product Id
        /// </summary>
        [ForeignKey("ProductId")]
        [ValidateNever]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// The ApplicationUserId
        /// </summary>
        [ForeignKey("UserId")]
        [ValidateNever]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; } = false;

        [NotMapped]  //for display purpose
        public double Price { get; set; }
    }
}
