using Microsoft.EntityFrameworkCore;
using MoviesLibrary.Core.Repositories;
using MoviesLibrary.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoviesLibrary.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T model)
        {
            await _context.AddAsync(model);
            _context.SaveChanges();

            return model;
        }
        public int Update(T model)
        {
            _context.Update(model);

            return _context.SaveChanges();
        }
        public int Delete(T model)
        {
            _context.Remove(model);

            return _context.SaveChanges();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T?> GetByCriteriaAsync(Expression<Func<T,bool>> expression)
        {
            IQueryable<T> query = _context.Set<T>().Where(expression);

            return await query.SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllByCriteriaAsync(Expression<Func<T,bool>> expression)
        {
            IQueryable<T> qurey = _context.Set<T>().Where(expression);

            return await qurey.ToListAsync();
        }
        public async Task<T?> GetByIncludeAsync(Expression<Func<T,bool>> expression, string[]? includes = null)
        {
            IQueryable<T> model = _context.Set<T>();

            if (includes is not null)
                foreach (var include in includes)
                    model = model.Include(include);

            return await model.SingleOrDefaultAsync(expression);
        }
    }
}
