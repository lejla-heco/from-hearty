
namespace Data.Repositories.Base
{
    public interface IUnitOfWork
    {
        public void Add<T>(T entity) where T : class, IModel;
        public void Update<T>(T entity) where T : class?, IModel?;
        public void Remove<T>(T entity) where T : class, IModel;
        public void Commit();
        public void AddRange<T>(IEnumerable<T> list) where T : class, IModel;
        public void UpdateRange<T>(IEnumerable<T> list) where T : class, IModel;
        public void RemoveRange<T>(IEnumerable<T> list) where T : class, IModel;
    }
}
