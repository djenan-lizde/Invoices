using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Invoices.Web.Services
{
    public interface IData<T> where T : class
    {
        List<T> Get();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
    }
}
