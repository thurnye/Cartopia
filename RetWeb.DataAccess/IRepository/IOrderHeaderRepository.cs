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
        
    }
}
