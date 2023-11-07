using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cartopia.Models.ViewModels
{
    public class ProductVM
    {
        /// <summary>
        /// Product Model
        /// </summary>
        public  Product Product { get; set; }

        /// <summary>
        /// This is the selection for the list of categories
        /// </summary>
        [ValidateNever]  //prevents it from validating
        public IEnumerable<SelectListItem> CategoryList { get; set; }   
    }
}
