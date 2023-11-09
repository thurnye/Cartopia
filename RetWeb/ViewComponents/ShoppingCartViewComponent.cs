using Cartopia.DataAccess.IRepository;
using Cartopia.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cartopia.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent   // this is the backend file for our view component
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //to get the loginIn User, use use the claimsIdentity, which has the nameIdentifer which will have the userId of the user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);    // this will retrieve the userId

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.UserId == claim.Value).Count());
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
