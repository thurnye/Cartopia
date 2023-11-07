using Cartopia.Models;

namespace Cartopia.DataAccess.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>   //this will implement the IRepository with the class ShoppingCart, this will include the Update
                                                                 // and the save exclusively for the ShoppingCart repository
    {
        /// <summary>
        ///  update the ShoppingCart repository
        /// </summary>
        /// <param name="obj"></param>
        void Update(ShoppingCart obj);
        
    }
}
