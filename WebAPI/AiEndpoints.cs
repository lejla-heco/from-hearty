using Microsoft.ML;
using static SampleClassification.ConsoleApp.SampleClassification;

static class AiEndpoints
{
    public static void MapAiEndpoints(this IEndpointRouteBuilder app) =>
        app.MapGet("/ai-service/{message}", (string message) =>
        {
            var mlContext = new MLContext();
            var mlModel = mlContext.Model.Load("../SampleClassification/SampleClassification.mlnet", out _);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            var input = new ModelInput
            {
                Col0 = message
            };

            var result = predEngine.Predict(input);

            var response = Convert.ToBoolean(result.PredictedLabel) ? "Positive" : "Negative";

            return response;
        });
}
