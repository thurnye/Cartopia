
using RetWeb.DataAccess.Data;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;
using RetWeb.Models.Models;

namespace RetWeb.DataAccess.Repository
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
