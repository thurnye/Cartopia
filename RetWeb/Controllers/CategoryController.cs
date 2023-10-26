using Microsoft.AspNetCore.Mvc;
using RetWeb.DataAccess.Data;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;

namespace RetWeb.Controllers
{
    public class CategoryController : Controller
    {   
        /// <summary>
        /// use the ICategoryRepository rather than use the ApplicationDbContext here directly
        /// </summary>
        private readonly ICategoryRepository _categoryRepo;


        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();  // retrieve the list
            return View(objCategoryList);
        }

        /// <summary>
        /// Get Displays for Create or Edit Category Page
        /// <param name="id"></param>
        /// </summary>
        /// <returns> the view page for create or edit </returns>
        public IActionResult CreateAndEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }


            Category? category = _categoryRepo.Get(u => u.Id == id); 
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }



        /// <summary>
        /// Post and Update method for the create category form
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
                //CREATE

                // Prevent the category name and the display order to have the same name
                if (obj.Name?.ToLower() == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "The Display Order and Category Name cannot be the same");
                    return View(obj); // Return the view with validation error
                }

                // Category creation
                _categoryRepo.Add(obj);
                message = obj.Name + " Category created successfully.";

            }
            else   //UPDATE
            {
                
                // Check if the category with the provided ID exists
                var existingCategory = _categoryRepo.Get(u => u.Id == obj.Id);
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

                _categoryRepo.Update(existingCategory);
                message = obj.Name + " Category updated successfully.";

            }

            _categoryRepo.Save();
            TempData["success"] = message;    // this will send back a message notification to the index category page 
            return RedirectToAction("Index", "Category");
        }


        /// <summary>
        /// Get Delete a Category UI
        /// <param name="id"></param>
        /// </summary>
        /// <returns> Delete Page </returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }


            Category? category = _categoryRepo.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }



        /// <summary>
        /// Delete a Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_categoryRepo.Remove(obj);
            obj.IsDeleted = true;

            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            TempData["success"] = "Deleted " + obj.Name + " Category Successfully.";
            ;
            return RedirectToAction("Index", "Category");
        }

    }
}
