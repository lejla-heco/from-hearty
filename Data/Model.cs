using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "blogging.db");
        Console.WriteLine($"DbPath: {DbPath}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");
}

public class Blog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Url { get; set; } = string.Empty;

    public List<Post> Posts { get; } = [];
}

public class Post
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Title { get; set; } = string.Empty;
    public required string Content { get; set; } = string.Empty;

    public Guid BlogId { get; set; }
    public Blog Blog { get; set; } = default!;
}