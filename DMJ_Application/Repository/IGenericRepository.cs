using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DMJ_Application.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includes = null);
        T GetById(Expression<Func<T, bool>> filter, string? includes = null);
        void Add(T entity);
        void Remove(T entity);

    }
}
