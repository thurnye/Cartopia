using Cartopia.DataAccess.IRepository;
using Cartopia.Models;
using Cartopia.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
		public IActionResult GetAll( string status)
		{
			

            IEnumerable<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "User").ToList();  // retrieve the list based on the status
			return Json(new { data = GetOrderHeaderByStatus(objOrderHeaders, status) });
		}

		
		private IEnumerable<OrderHeader> GetOrderHeaderByStatus(IEnumerable<OrderHeader> objOrderHeaders, string status)
		{
            switch (status)
            {
                case "pending":
                  return objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment || u.PaymentStatus == SD.StatusPending);
                   
                case "inprocess":
                    return objOrderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
                    
                case "completed":
                    return objOrderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
                    
                case "approved":
                    return  objOrderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
                   
                default:
                   return objOrderHeaders;
            }
		}

		#endregion
	}

}
