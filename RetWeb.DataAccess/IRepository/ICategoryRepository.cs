using Cartopia.Models;

namespace Cartopia.DataAccess.IRepository
{
    public interface ICategoryRepository : IRepository<Category>   //this will implement the IRepository with the class Category, this will include the Update
                                                                   // and the save exclusively for the category repository
    {
        /// <summary>
        ///  update the category repository
        /// </summary>
        /// <param name="obj"></param>
        void Update(Category obj);
        
    }
}
