static class Endpoints
{
    public record BlogDto(string Url);
    public record PostDto(string Title, string Content, Guid BlogId);

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/post", (PostDto postDto, BloggingContext context) =>
        {
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                BlogId = postDto.BlogId
            };

            context.Posts.Add(post);
            context.SaveChanges();

            return post;
        });

        app.MapPost("/blog", (BlogDto blogDto, BloggingContext context) =>
        {
            var blog = new Blog
            {
                Url = blogDto.Url
            };

            context.Blogs.Add(blog);
            context.SaveChanges();

            return blog;
        });

        app.MapGet("/blogs", (BloggingContext context) => context.Blogs.ToArray());

        app.MapGet("/posts", (BloggingContext context) => context.Posts.ToArray());
    }
}
