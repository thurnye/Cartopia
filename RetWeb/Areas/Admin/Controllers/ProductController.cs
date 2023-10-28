using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;
using RetWeb.Models.ViewModels;

namespace RetWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        /// <summary>
        /// use the IProductRepository rather than use the ApplicationDbContext here directly
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;


        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().Where(c => c.IsDeleted == false).ToList();  // retrieve the list
           return View(objProductList);
        }

        /// <summary>
        /// Get Displays for Create or Edit Product Page
        /// <param name="id"></param>
        /// </summary>
        /// <returns> the view page for create or edit </returns>
        public IActionResult CreateAndEdit(int? id)
        {
            //we need the list of categories in the index view for dropdown option so we use the SelectListItem and convert it to dropdown options using projection
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            // to transfer the data to the view we have 2 options
            // 1. ViewBag, which transfers data from the controller to view, not vice-versa. Ideal for situations in which the temporary data is not in a model Product
            //ViewBag.CategoryList = CategoryList;

            //2. ViewData transfers data from the controller to view, not vice-versa, Ideal for situations in which the temporary data is not in a model.
            //ViewData["CategoryList"] = CategoryList;

            //3. TempData can be used to store data between two consecutive request and it internally uses session to store the data.

            //4. Recommended-- is to bind the data together in what is called viewModel, which is specific for a view, for this to be achieved, we need to create a combine product and 
            /// categorylist model called ProductVM

            ProductVM productVM = new() 
            { 
              CategoryList = CategoryList
            };
            if (id == null || id == 0)
            {
                productVM.Product = new Product();
                return View(productVM);
            }


            Product? Product = _unitOfWork.Product.Get(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            productVM.Product = Product;

            return View(productVM);
        }



        /// <summary>
        /// Post and Update method for the create Product form
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateAndEdit(ProductVM obj)   // we have to modify the model to productVM
        {
           
            // Check if the ModelState is valid before proceeding
            if (!ModelState.IsValid)
            {
                return View(obj.Product);
            }

            string message;

            if (obj.Product.Id == 0 || obj.Product.Id == null)
            {
                //CREATE

                // Product creation
                _unitOfWork.Product.Add(obj.Product);
                message = obj.Product.Title + " created successfully.";

            }
            else   //UPDATE
            {

                // Check if the Product with the provided ID exists
                var existingProduct = _unitOfWork.Product.Get(u => u.Id == obj.Product.Id);
                if (existingProduct == null)
                {
                    return NotFound(); // Handle this situation according to your needs
                }

                // Update the Product with the provided data
                existingProduct.Title = obj.Product.Title;
                existingProduct.Author = obj.Product.Author;
                existingProduct.Description = obj.Product.Description;
                existingProduct.ISBN = obj.Product.ISBN;
                existingProduct.ListPrice = obj.Product.ListPrice;
                existingProduct.Price = obj.Product.Price;
                existingProduct.Price50 = obj.Product.Price50;
                existingProduct.Price100 = obj.Product.Price100;
                existingProduct.CategoryId = obj.Product.CategoryId;

                _unitOfWork.Product.Update(existingProduct);
                message = obj.Product.Title + " updated successfully.";

            }

            _unitOfWork.Save();
            TempData["success"] = message;    // this will send back a message notification to the index Product page 
            return RedirectToAction("Index", "Product");
        }


        /// <summary>
        /// Get Delete a Product UI
        /// <param name="id"></param>
        /// </summary>
        /// <returns> Delete Page </returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }


            Product? Product = _unitOfWork.Product.Get(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }



        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_unitOfWork.Product.Remove(obj);
            obj.IsDeleted = true;

            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Deleted " + obj.Title + " Product Successfully.";
            ;
            return RedirectToAction("Index", "Product");
        }

    }
}
