using Microsoft.AspNetCore.Mvc;
using RetWeb.Data;
using RetWeb.Models;

namespace RetWeb.Controllers
{
    public class CategoryController : Controller
    {   
        /// <summary>
        ///  this will help us to connect to the db through the application context since it is available in the services
        /// </summary>
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();  // retrieve the list
            return View(objCategoryList);
        }

        /// <summary>
        /// Displays the Create Category Page
        /// </summary>
        /// <returns></returns>
        //public IActionResult Create() 
        //{
        //    return View();
        //}


        /// <summary>
        /// This handles the post method for the create category form
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public IActionResult Create(Category obj)
        //{   
        //    //prevent the category name and the display order to have the same name
        //    if(obj.Name?.ToLower() == obj.DisplayOrder.ToString())
        //    {
        //        ModelState.AddModelError("Name", "The Display Order and Category Name cannot be the same");
        //    }
        //    if (ModelState.IsValid) 
        //    { 
        //        _db.Categories.Add(obj);  // Add the category data to the category table
        //        _db.SaveChanges();
        //        return RedirectToAction("Index", "Category");  //Redirect to the category details page, params => (view name, ControllerName-optional)
        //    }
        //    return View();
        //}

        /// <summary>
        /// Get Displays for Create or Edit Category Page
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateAndEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }


            Category? category = _db.Categories.Find(id); // only accepts id or the primary key
            //Category category1 = _db.Categories.FirstOrDefault(u => u.Id == id);  // return null if object not found otherwise return the first found, accepts any field name
            //Category category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); // use the where and also the first or default
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        /// <summary>
        /// Post and Update method for the create category form
        /// </summary>
        [HttpPost]
        public IActionResult CreateAndEdit(Category obj)
        {
            // Check if the ModelState is valid before proceeding
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            if (obj.Id == 0 || obj.Id == null)
            {
                // Prevent the category name and the display order to have the same name
                if (obj.Name?.ToLower() == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "The Display Order and Category Name cannot be the same");
                    return View(obj); // Return the view with validation error
                }

                // Category creation
                _db.Categories.Add(obj);
            }
            else   //UPDATE
            {
                
                // Check if the category with the provided ID exists
                var existingCategory = _db.Categories.Find(obj.Id);
                if (existingCategory == null)
                {
                    return NotFound(); // Handle this situation according to your needs
                }

                // Prevent the category name and the display order to have the same name
                if (obj.Name?.ToLower() == obj.DisplayOrder.ToString() && obj.Name != existingCategory.Name)
                {
                    ModelState.AddModelError("Name", "The Display Order and Category Name cannot be the same");
                    return View(obj); // Return the view with validation error
                }

                // Update the category with the provided data
                existingCategory.Name = obj.Name;
                existingCategory.DisplayOrder = obj.DisplayOrder;

                _db.Categories.Update(existingCategory);
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

    }
}
