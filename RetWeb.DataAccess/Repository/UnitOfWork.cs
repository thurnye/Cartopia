﻿using RetWeb.DataAccess.Data;
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

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
