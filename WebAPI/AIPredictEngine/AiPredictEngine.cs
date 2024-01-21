using Microsoft.ML;
using static ExampleMachineLearning.ConsoleApp.ExampleMachineLearning;

namespace WebAPI.AIPredictEngine
{
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
                    ITransformer mlModel = default!;

#if DEBUG
                    mlModel = mlContext.Model.Load("../HeartyBase/ExampleMachineLearning.mlnet", out _);
#else
                    mlModel = mlContext.Model.Load("./ExampleMachineLearning.mlnet", out _);
#endif

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
}