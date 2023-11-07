using Cartopia.DataAccess.IRepository;
using Cartopia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cartopia.Areas.Admin.Controllers
{
	[Area("admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		#region API Calls 
		
		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "User").ToList();  // retrieve the list
			return Json(new { data = objOrderHeaders });
		}

		
		#endregion
	}
}
