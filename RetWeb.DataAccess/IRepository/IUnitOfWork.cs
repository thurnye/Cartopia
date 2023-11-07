

namespace Cartopia.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// User 
        /// </summary>
        IUserRepository User { get; }


        /// <summary>
        /// Category 
        /// </summary>
        ICategoryRepository Category { get; }

        /// <summary>
        /// Product 
        /// </summary>
        IProductRepository Product { get; }

        /// <summary>
        /// Company 
        /// </summary>
        ICompanyRepository Company { get; }

        /// <summary>
        /// ShoppingCarty 
        /// </summary>
        IShoppingCartRepository ShoppingCart { get; }

        /// <summary>
        /// OrderHeader 
        /// </summary>
        IOrderHeaderRepository OrderHeader { get; }

        /// <summary>
        /// OrderDetail 
        /// </summary>
        IOrderDetailRepository OrderDetail { get; }

        void Save();
    }
}
