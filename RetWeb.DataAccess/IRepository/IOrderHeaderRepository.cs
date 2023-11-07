using RetWeb.Models;

namespace RetWeb.DataAccess.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>   //this will implement the IRepository with the class OrderHeader, this will include the Update
                                                                   // and the save exclusively for the OrderHeader repository
    {
        /// <summary>
        ///  update the OrderHeader repository
        /// </summary>
        /// <param name="obj"></param>
        void Update(OrderHeader obj);

        /// <summary>
        /// Method for updating only payment status or order status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderStatus"></param>
        /// <param name="paymentStatus"></param>
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        /// <summary>
        /// Updating the stripe payment details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sessionId"></param>
        /// <param name="paymentIntentId"></param>
        void UpdateStripePaymentID(int id, string sessionId, string? paymentIntentId);
        
    }
}
