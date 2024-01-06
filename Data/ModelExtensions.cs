using Microsoft.EntityFrameworkCore;

public static class ModelExtensions
{
    public static void AddOrUpdate<T>(this DbSet<T> dbSet, List<T> data) where T : class, IModel =>
        data.ForEach(d =>
        {
            var entity = dbSet.Find(d.Id);
            if (entity == null)
                dbSet.Add(d);
        });
}


