using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RetWeb.Models
{
    public class Category
    {
        /// <summary>
        /// Id of the Category
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the Category
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Display Order of the Category
        /// </summary>
        [DisplayName("Display Order")]
        public int? DisplayOrder { get; set; }




        
    }
}
