using Task_GTS.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Task_GTS.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public void Create(T entity);
        public void Alter(T entity);
        public void Delete(T entity);
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true);
        public T? GetOne(Expression<Func<T, bool>>? filter, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true);

        public void Commit();
    }
}
