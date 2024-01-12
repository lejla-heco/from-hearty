using Microsoft.ML;
using static ExampleMachineLearning.ConsoleApp.ExampleMachineLearning;

public static class AiPredictEngine
{
    private static PredictionEngine<ModelInput, ModelOutput>? _predictEngine = null;
    public static PredictionEngine<ModelInput, ModelOutput> PredictEngine
    {
        get
        {
            if (_predictEngine == null)
            {
                var mlContext = new MLContext();
                var mlModel = mlContext.Model.Load("../ExampleMachineLearning/ExampleMachineLearning.mlnet", out _);
                _predictEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            }

            return _predictEngine;
        }
    }

    public static bool Predict(string message)
    {
        var input = new ModelInput
        {
            Col0 = message
        };

        var result = PredictEngine.Predict(input);

        return Convert.ToBoolean(result.PredictedLabel);
    }
}