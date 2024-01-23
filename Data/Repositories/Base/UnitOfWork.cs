
namespace Data.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;

        public UnitOfWork(
            MyContext context
        )
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class, IModel
        {
            _context.Set<T>().Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        void IUnitOfWork.AddRange<T>(IEnumerable<T> list)
        {
            _context.Set<T>().AddRange(list);
        }

        void IUnitOfWork.Remove<T>(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        void IUnitOfWork.RemoveRange<T>(IEnumerable<T> list)
        {
            _context.Set<T>().RemoveRange(list);
        }

        void IUnitOfWork.Update<T>(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        void IUnitOfWork.UpdateRange<T>(IEnumerable<T> list)
        {
            _context.Set<T>().UpdateRange(list);
        }
    }
}
