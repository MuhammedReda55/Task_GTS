using Task_GTS.Data;
using Task_GTS.Models;
using Task_GTS.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Task_GTS.Repository
{
    public class Repositroy<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;

        public Repositroy(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            
        }

        public void Alter(T entity)
        {
            _dbSet.Update(entity);
            
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
           
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (includeProps != null)
            {
                foreach (var item in includeProps)
                {
                    query = query.Include(item);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public T? GetOne(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true)
        {
            return Get(filter,includeProps,tracked).FirstOrDefault();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        } 
    }
}
