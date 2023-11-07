using RetWeb.DataAccess.Data;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetWeb.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;


        /// <summary>
        /// ApplicationUser 
        /// </summary>
        public IUserRepository User { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public ICategoryRepository Category { get; private set; }

        /// <summary>
        /// Product
        /// </summary>
        public IProductRepository Product { get; private set; }

        /// <summary>
        /// Company
        /// </summary>
        public ICompanyRepository Company { get; private set; }
        
        /// <summary>
        /// ShoppingCart
        /// </summary>
        public IShoppingCartRepository ShoppingCart { get; private set; }

        /// <summary>
        /// OrderHeader
        /// </summary>
        public IOrderHeaderRepository OrderHeader { get; private set; }

        /// <summary>
        /// OrderDetail
        /// </summary>
        public IOrderDetailRepository OrderDetail { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
