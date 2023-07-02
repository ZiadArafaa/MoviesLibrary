using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task CreateAsync(T model);
        public void Update(T model);
        public void Delete(T model);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(object id);
        public Task<T?> GetByCriteriaAsync(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> GetAllByCriteriaAsync(Expression<Func<T, bool>> expression);
        public Task<T?> GetByIncludeAsync(Expression<Func<T, bool>> expression, string[]? includes = null);
    }
}
