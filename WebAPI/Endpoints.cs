record MyClass(string Name) { }

public class MyService
{
    public string CreateMessage(string message)
    {
        return $"Hello {message}";
    }
}

static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/string", () => "Hello World!");

        app.MapGet("/anonymousObject{name}", (string name) => new { Message = $"Hello {name}!" });
        
        app.MapGet("/class", () => new MyClass("This is a record."));
        
        app.MapGet("/service", (MyService myService) => {
            var myServiceMessage = myService.CreateMessage("World");
            return new MyClass(myServiceMessage);
        });
        
        app.MapPost("/post", (MyClass myClass) => myClass);
    }
}
