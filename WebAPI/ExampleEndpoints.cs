static class ExampleEndpoints
{
    record MyClass(string Name) { }

    public class MyService
    {
        public string CreateMessage(string message)
        {
            return $"Hello {message}";
        }
    }

    public static void MapExampleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/string", () => "Hello World!");

        app.MapGet("/anonymousObject/{name}", (string name) => new { Message = $"Hello {name}!" });

        app.MapGet("/class", () => new MyClass("This is a record."));

        app.MapGet("/service", (MyService myService) =>
        {
            var myServiceMessage = myService.CreateMessage("World");
            return new MyClass(myServiceMessage);
        });
    }
}
