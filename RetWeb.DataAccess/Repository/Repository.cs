using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetWeb.DataAccess.Data;
using RetWeb.DataAccess.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RetWeb.DataAccess.Repository
{   

    public class Repository<T> : IRespository<T> where T : class
    {
        // we need to have access to the dbContext using dependency injection
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;                // here we create an internal db set on the generic type T because we want the type to be generic. and it will have all the records 
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();   
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        /// <summary>
        /// Get single Entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;  
            query =  query.Where(filter);
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Get All entity
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        /// <summary>
        /// Remove One entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Remove Collections of entity
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Update One Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
