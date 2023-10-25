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
            List<Category> objCategoryList = _db.Categories.Where(c => c.IsDeleted == 0).ToList();  // retrieve the list
            return View(objCategoryList);
        }

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

            string message;

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
                message = obj.Name + " Category created successfully.";

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
                message = obj.Name + " Category updated successfully.";

            }

            _db.SaveChanges();
            TempData["success"] = message;    // this will send back a message notification to the index category page 
            return RedirectToAction("Index", "Category");
        }


        /// <summary>
        /// Get Delete a Category UI
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }


            Category? category = _db.Categories.Find(id); 
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        /// <summary>
        /// Delete a Category
        /// </summary>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(obj);
            obj.IsDeleted = 1;

            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Deleted " + obj.Name + " Category Successfully.";
            ;
            return RedirectToAction("Index", "Category");
        }

    }
}
