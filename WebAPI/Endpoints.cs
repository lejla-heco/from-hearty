static class Endpoints
{
    public record BlogDto(string Url);
    public record PostDto(string Title, string Content, Guid BlogId);

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        // app.MapPost("/post", (PostDto postDto, MyContext context) =>
        // {
        //     var post = new Post
        //     {
        //         Title = postDto.Title,
        //         Content = postDto.Content,
        //         BlogId = postDto.BlogId
        //     };

        //     context.Posts.Add(post);
        //     context.SaveChanges();

        //     return post;
        // });

        // app.MapPost("/blog", (BlogDto blogDto, MyContext context) =>
        // {
        //     var blog = new Blog
        //     {
        //         Url = blogDto.Url
        //     };

        //     context.Blogs.Add(blog);
        //     context.SaveChanges();

        //     return blog;
        // });

        // app.MapGet("/blogs", (MyContext context) => context.Blogs.ToArray());

        // app.MapGet("/posts", (MyContext context) => context.Posts.ToArray());
    }
}
