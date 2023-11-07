using RetWeb.Models;

namespace RetWeb.DataAccess.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>   //this will implement the IRepository with the class OrderDetail, this will include the Update
                                                                   // and the save exclusively for the OrderDetail repository
    {
        /// <summary>
        ///  update the OrderDetail repository
        /// </summary>
        /// <param name="obj"></param>
        void Update(OrderDetail obj);
        
    }
}
