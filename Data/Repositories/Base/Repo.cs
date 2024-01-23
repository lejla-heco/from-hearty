using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories.Base
{
    public class Repo<T> : IRepo<T> where T : class, IModel
    {
        protected readonly MyContext _context;

        public Repo(MyContext context)
        {
            _context = context;
        }

        public T GetByGuid(Guid id)
        {
            return _context.Set<T>().SingleOrDefault(t => t.Id == id);
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetByGuids(IEnumerable<Guid> ids)
        {
            return _context.Set<T>().Where(t => ids.Contains(t.Id)).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public int Count(Expression<Func<T, bool>> filterExpression = null)
        {
            return filterExpression == null
                ? _context.Set<T>().Count()
                : _context.Set<T>().Count(filterExpression);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            return filterExpression == null
                ? _context.Set<T>().CountAsync()
                : _context.Set<T>().CountAsync(filterExpression);
        }
    }
}
