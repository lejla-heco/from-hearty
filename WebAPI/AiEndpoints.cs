using static ExampleMachineLearning.ConsoleApp.ExampleMachineLearning;

static class AiExampleEndpoints
{
    public static void MapAiExampleEndpoints(this IEndpointRouteBuilder app) =>
        app.MapGet("/ai-service/{message}", (string message) =>
        {
            var predEngine = AiPredictEngine.PredictEngine;
            var input = new ModelInput
            {
                Col0 = message
            };

            var result = predEngine.Predict(input);

            var response = Convert.ToBoolean(result.PredictedLabel) ? "Positive" : "Negative";

            return response;
        });
}
