using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Cartopia.DataAccess.IRepository;
using Cartopia.Models;
using Cartopia.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using Cartopia.Utility;
using Microsoft.AspNetCore.Http;

namespace Cartopia.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get the home Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").Where(c => c.IsDeleted == false);
            return View(productList);
        }

        /// <summary>
        /// Get Single Product with ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");

            ShoppingCart cart = new()  //To adjust the Detail page to be able to send in the quantity for specific product, we have to modify the 
                                       //to use ShoppingCart.
            {
                Product = product,
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]  //only authorized users are allowed to post
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            //to get the loginIn User, use use the claimsIdentity, which has the nameIdentifer which will have the userId of the user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;    // this will retrieve the userId
            shoppingCart.UserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ProductId == shoppingCart.ProductId && u.UserId == userId);

            if(cartFromDb != null)
            {   //update the cart
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
            //Add the shopping cart record
            _unitOfWork.ShoppingCart.Add(shoppingCart);
                //when we add a new item to the cart, we will be adding the value to session with the total number of cart items the  user has
            }
            _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.UserId == userId).Count());
            TempData["success"] = "Added to Cart successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
