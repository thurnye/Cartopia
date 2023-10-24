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
        public IActionResult Create() 
        {
            return View();
        }
        /// <summary>
        /// This handles the post method for the create category form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            _db.Categories.Add(obj);  // Add the category data to the category table
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");  //Redirect to the category details page, params => (view name, ControllerName-optional)
        }
    }
}
