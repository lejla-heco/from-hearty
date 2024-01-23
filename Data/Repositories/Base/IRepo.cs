
using System.Linq.Expressions;

namespace Data.Repositories.Base
{
    public interface IRepo
    {
    }
    public interface IRepo<T> : IRepo where T: class, IModel
    {
        int Count(Expression<Func<T, bool>> filterExpression = null);
        T GetByGuid(Guid guid);
        IEnumerable<T> GetByGuids(IEnumerable<Guid> guids);
        IEnumerable<T> GetAll();

        Task<int> CountAsync(Expression<Func<T, bool>> filterExpression = null);
        Task<T> GetByGuidAsync(Guid guid);
        Task<List<T>> GetAllAsync();
    }
}
