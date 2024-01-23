
using var db = new MyContext();

db.SetUpData();

// // Read
// var blog = db.Blogs
//     .OrderBy(b => b.Id)
//     .First();

// // Update
// blog.Url = "https://devblogs.microsoft.com/dotnet";
// blog.Posts.Add(new Post
// {
//     Title = "Hello World",
//     Content = "I wrote an app using EF Core!"
// });
// db.SaveChanges();

// // Delete
// db.Remove(blog);
// db.SaveChanges();

// Delete All
// db.RemoveRange(db.HouseDoctors);
// db.RemoveRange(db.Findings);
// db.RemoveRange(db.Cardiologists);
// db.RemoveRange(db.Patients);
// db.SaveChanges();
