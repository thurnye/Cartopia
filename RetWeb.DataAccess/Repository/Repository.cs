using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Cartopia.DataAccess.Data;
using Cartopia.DataAccess.IRepository;

namespace Cartopia.DataAccess.Repository    // this will be a generic Repository for the app, here we wont add the updates can be tailored to fit specific request
{

    public class Repository<T> : IRepository<T> where T : class   
    {
        // we need to have access to the dbContext using dependency injection
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;                // here we create an internal db set on the generic type T because we want the type to be generic. and it will have all the records 
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);  // this will include all the categories with the respective foreign keys. The include can have more properties.
        }                                                                       //in this case we want to include the Category or populate the data in category

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
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;

            if (tracked){
                 query = dbSet;
            }
            else{
                query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

       
        /// <summary>
        /// Get all 
        /// </summary>
        /// <param name="includeProperties"></param> the name of the property we want to populate
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    
                query = query.Include(includeProp);
                }
            }
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

    }
}
