using Cartopia.Models;

namespace Cartopia.DataAccess.IRepository
{
    public interface IProductRepository : IRepository<Product>   //this will implement the IRepository with the class Product, this will include the Update
                                                                   // and the save exclusively for the Product repository
    {
        /// <summary>
        ///  update the Product repository
        /// </summary>
        /// <param name="obj"></param>
        void Update(Product obj);
        
    }
}
