﻿using Cartopia.DataAccess.Data;
using Cartopia.DataAccess.IRepository;
using Cartopia.Models;

namespace Cartopia.DataAccess.Repository
{
    //we already have the add, get, remove and removerange in the our generic repo, so we dont need them we need the base functionality 
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;           
        public ProductRepository(ApplicationDbContext db) : base(db) //we want to bass the applicationDbContext to the base class which is the Repository.cs file
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
