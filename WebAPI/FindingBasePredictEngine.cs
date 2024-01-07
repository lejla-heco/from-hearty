using Microsoft.ML;
using static HeartyBase.ConsoleApp.HeartyBase;

public static class FindingBasePredictEngine
{
    private static PredictionEngine<ModelInput, ModelOutput>? _predictEngine = null;
    public static PredictionEngine<ModelInput, ModelOutput> PredictEngine
    {
        get
        {
            if (_predictEngine == null)
            {
                var mlContext = new MLContext();
                var mlModel = mlContext.Model.Load("../HeartyBase/HeartyBase.mlnet", out _);
                _predictEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            }

            return _predictEngine;
        }
    }

    public static bool Predict(FindingBase findingBase)
    {
        var input = new ModelInput
        {
            BMI = findingBase.BMI,
            Smoking = findingBase.Smoking,
            AlcoholDrinking = findingBase.AlcoholDrinking,
            Stroke = findingBase.Stroke,
            PhysicalHealth = findingBase.PhysicalHealth,
            MentalHealth = findingBase.MentalHealth,
            DiffWalking = findingBase.DiffWalking,
            Sex = findingBase.Sex,
            AgeCategory = findingBase.AgeCategory,
            Diabetic = findingBase.Diabetic,
            PhysicalActivity = findingBase.PhysicalActivity,
            GenHealth = findingBase.GenHealth,
            SleepTime = findingBase.SleepTime,
            Asthma = findingBase.Asthma,
            KidneyDisease = findingBase.KidneyDisease,
            SkinCancer = findingBase.SkinCancer,
        };

        var result = PredictEngine.Predict(input);

        Console.WriteLine(result.PredictedLabel);
        Console.WriteLine(result.HeartDisease);

        return Convert.ToBoolean(result.PredictedLabel);
    }
}