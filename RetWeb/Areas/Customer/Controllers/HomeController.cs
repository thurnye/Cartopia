using Microsoft.AspNetCore.Mvc;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;
using RetWeb.Models.ViewModels;
using System.Diagnostics;

namespace RetWeb.Areas.Customer.Controllers
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
