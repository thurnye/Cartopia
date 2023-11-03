

namespace RetWeb.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
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

        void Save();
    }
}
