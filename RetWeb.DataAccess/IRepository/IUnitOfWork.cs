

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

        void Save();
    }
}
