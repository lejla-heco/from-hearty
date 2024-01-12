using var db = new BloggingContext();

// Create
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
db.SaveChanges();

// Read
var blog = db.Blogs
    .OrderBy(b => b.Id)
    .First();

// Update
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(new Post
{
    Title = "Hello World",
    Content = "I wrote an app using EF Core!"
});
db.SaveChanges();

// Delete
db.Remove(blog);
db.SaveChanges();

// Delete All
// db.RemoveRange(db.Posts);
// db.RemoveRange(db.Blogs);
// db.SaveChanges();
