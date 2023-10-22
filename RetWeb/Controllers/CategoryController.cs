using Microsoft.AspNetCore.Mvc;

namespace RetWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
