using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    interface IGenericRepository<T> where T : class
    {
        T Add(T entity);

        T[] GetAll();

        T GetById(int id);

        void Update(T entity);

        void Delete(T entity);

        /// <summary>
        /// Return result depens on predicate and count
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="count">Munber of last added item</param>
        /// <returns></returns>
        T[] Get(Expression<Func<T, bool>> predicate = null, int count = 0);
    }
}
