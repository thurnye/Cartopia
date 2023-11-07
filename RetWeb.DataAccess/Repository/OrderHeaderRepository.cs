﻿
using RetWeb.DataAccess.Data;
using RetWeb.DataAccess.IRepository;
using RetWeb.Models;

namespace RetWeb.DataAccess.Repository
{
    //we already have the add, get, remove and removerange in the our generic repo, so we dont need them we need the base functionality 
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;           
        public OrderHeaderRepository(ApplicationDbContext db) : base(db) //we want to bass the applicationDbContext to the base class which is the Repository.cs file
        {
            _db = db;
        }


        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
    }
}
