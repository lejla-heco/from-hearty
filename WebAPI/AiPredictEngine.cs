using Microsoft.ML;
using static SampleClassification.ConsoleApp.SampleClassification;

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
                var mlModel = mlContext.Model.Load("../SampleClassification/SampleClassification.mlnet", out _);
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