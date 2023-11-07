using Cartopia.Models;

namespace Cartopia.DataAccess.IRepository
{
    public interface ICompanyRepository : IRepository<Company>   //this will implement the IRepository with the class Company, this will include the Update
                                                                   // and the save exclusively for the Company repository
    {
        /// <summary>
        ///  update the Product repository
        /// </summary>
        /// <param name="obj"></param>
        void Update(Company obj);
        
    }
}
