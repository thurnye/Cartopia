using Cartopia.DataAccess.IRepository;
using Cartopia.Models;
using Cartopia.Models.ViewModels;
using Cartopia.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Cartopia.Areas.Admin.Controllers
{
	[Area("admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Details(int orderId)
        {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "User"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            return View(OrderVM);
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + ","+ SD.Role_Employee)]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.Street = OrderVM.OrderHeader.Street;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.State = OrderVM.OrderHeader.State;
            orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if(!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }   
            if(!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Order Details Updated Successfully.";


            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
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
