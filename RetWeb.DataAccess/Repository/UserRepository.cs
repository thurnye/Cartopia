
using Cartopia.DataAccess.Data;
using Cartopia.DataAccess.IRepository;
using Cartopia.Models;
using Cartopia.Models.Models;

namespace Cartopia.DataAccess.Repository
{
    //we already have the add, get, remove and removerange in the our generic repo, so we dont need them we need the base functionality 
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;           
        public UserRepository(ApplicationDbContext db) : base(db) //we want to bass the applicationDbContext to the base class which is the Repository.cs file
        {
            _db = db;
        }
    }
}
