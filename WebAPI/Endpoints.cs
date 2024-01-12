using Microsoft.ML;
using static SampleClassification.ConsoleApp.SampleClassification;

record MyClass(string Name) { }

public class MyService
{
    public string CreateMessage(string message)
    {
        return $"Hello {message}";
    }
}

public class MyAiService
{
    public record MyAiResponse(string Response) { }
    public MyAiResponse CreateMessage(string message)
    {
        var mlContext = new MLContext();
        var mlModel = mlContext.Model.Load("../SampleClassification/SampleClassification.mlnet", out _);

        var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

        var input = new ModelInput
        {
            Col0 = message
        };

        ModelOutput result = predEngine.Predict(input);

        var response = Convert.ToBoolean(result.PredictedLabel) ? "Positive" : "Negative";

        return new MyAiResponse(response);
    }
}

static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/string", () => "Hello World!");

        app.MapGet("/anonymousObject{name}", (string name) => new { Message = $"Hello {name}!" });

        app.MapGet("/class", () => new MyClass("This is a record."));

        app.MapGet("/service", (MyService myService) =>
        {
            var myServiceMessage = myService.CreateMessage("World");
            return new MyClass(myServiceMessage);
        });

        app.MapGet("/ai-service{message}", (MyAiService myAiService, string message) =>
        {
            var myServiceMessage = myAiService.CreateMessage(message);
            return myServiceMessage;
        });

        app.MapPost("/post", (MyClass myClass) => myClass);
    }
}
