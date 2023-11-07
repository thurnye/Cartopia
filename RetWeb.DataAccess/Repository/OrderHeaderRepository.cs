
using RetWeb.DataAccess.Data;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;

namespace RetWeb.DataAccess.Repository
{
    //we already have the add, get, remove and removerange in the our generic repo, so we dont need them we need the base functionality 
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;           
        public OrderHeaderRepository(ApplicationDbContext db) : base(db) //we want to bass the applicationDbContext to the base class which is the Repository.cs file
        {
            _db = db;
        }


        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(orderFromDb == null)
            {
                orderFromDb.OrderStatus = orderStatus;

                if(!string.IsNullOrEmpty(paymentStatus)) 
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                };
            }
		}

		public void UpdateStripePaymentID(int id, string sessionId, string? paymentIntentId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if(!string.IsNullOrEmpty(sessionId))   // generates when a user tries to make a payment
            {
                orderFromDb.SessionId = sessionId;
            }
            if(!string.IsNullOrEmpty(paymentIntentId))   // if payment is  successful then a paymentIntentId is generated
            {
                orderFromDb.SessionId = sessionId;  //update a successful payment
                orderFromDb.PaymentDate = DateTime.Now;
            }
		}
	}
}
