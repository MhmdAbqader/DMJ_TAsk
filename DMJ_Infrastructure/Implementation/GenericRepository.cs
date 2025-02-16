using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DMJ_Application.Repository;
using DMJ_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMJ_Infrastructure.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
     

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includes = null)
        {            
            IQueryable<T> query = _dbSet ;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var inc in includes.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.ToList();
        }

        public T GetById(Expression<Func<T, bool>> filter, string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                foreach (var inc in includes.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(inc.Trim());
                }
            }
            return query.FirstOrDefault();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity); 
        }
    }
}
