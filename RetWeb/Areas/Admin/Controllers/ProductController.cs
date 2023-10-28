using Microsoft.AspNetCore.Mvc;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;

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
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();  // retrieve the list
            return View(objProductList);
        }

        /// <summary>
        /// Get Displays for Create or Edit Product Page
        /// <param name="id"></param>
        /// </summary>
        /// <returns> the view page for create or edit </returns>
        public IActionResult CreateAndEdit(int? id)
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
        /// Post and Update method for the create Product form
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateAndEdit(Product obj)
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

                // Product creation
                _unitOfWork.Product.Add(obj);
                message = obj.Title + " created successfully.";

            }
            else   //UPDATE
            {

                // Check if the Product with the provided ID exists
                var existingProduct = _unitOfWork.Product.Get(u => u.Id == obj.Id);
                if (existingProduct == null)
                {
                    return NotFound(); // Handle this situation according to your needs
                }

                // Update the Product with the provided data
                existingProduct.Title = obj.Title;
                existingProduct.Author = obj.Author;
                existingProduct.Description = obj.Description;
                existingProduct.ISBN = obj.ISBN;
                existingProduct.ListPrice = obj.ListPrice;
                existingProduct.Price = obj.Price;
                existingProduct.Price50 = obj.Price50;
                existingProduct.Price100 = obj.Price100;

                _unitOfWork.Product.Update(existingProduct);
                message = obj.Title + " updated successfully.";

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
