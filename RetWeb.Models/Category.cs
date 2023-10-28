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
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string? Name { get; set; }

        /// <summary>
        /// Display Order of the Category
        /// </summary>
        [DisplayName("Display Order")] //This will be the name of the field in UI
        [Range( 1, 100, ErrorMessage="Display Order must be between 1 and 100")]  // This is for validation of the value
        public int DisplayOrder { get; set; }


        /// <summary>
        /// For Displaying or removing items from the ui
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
