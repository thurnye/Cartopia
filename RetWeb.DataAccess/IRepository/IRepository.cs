using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RetWeb.DataAccess.IRepository
{
    public interface IRepository<T> where T : class  // since we are working with generic interface and do not know what the class Type will be we will give it a Type <T>
    {                                               //T- Category or any other generic model which we want to perform the operation or want to interact with the dbContext
                                                    // this will be a generic Interface Repository for the app
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns> list of all categories</returns>
        IEnumerable<T> GetAll();


        /// <summary>
        ///  Get a single category
        /// </summary>
        /// <returns>single category</returns>
        /// // here we pass a function that returns a boolean.
        // we choose this to be more flexible in what parameter we choose to pass for an individual record.
        // think about it like the same syntax as FirstOrDefault
        T Get(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Add method
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// Delete a collection of entities
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);

    }
}
