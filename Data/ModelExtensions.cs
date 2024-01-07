using Microsoft.EntityFrameworkCore;

public static class ModelExtensions
{
    public static void AddOrUpdateRange<T>(this DbSet<T> dbSet, List<T> data) where T : class, IModel =>
        data.ForEach(d =>
        {
            var entity = dbSet.Find(d.Id);
            if (entity == null)
                dbSet.Add(d);
        });

    public static void AddOrUpdate<T>(this DbSet<T> dbSet, T data) where T : class, IModel
    {
        var entity = dbSet.AsNoTracking().FirstOrDefault(x => x.Id == data.Id);
        if (entity == null)
            dbSet.Add(data);
        else
            dbSet.Update(data);
    }
}
