using FromHeartyAI.DataStructures;
using Microsoft.ML;

namespace FromHeartyAI.ML_Model
{
    public class MLModel
    {
        private readonly MLContext mlContext;
        private ITransformer trainedModel = null;

        private static string BaseDatasetsRelativePath = $"{AppDomain.CurrentDomain.BaseDirectory}DataSets";
        private static string TrainDataRelativePath = $"{BaseDatasetsRelativePath}/FromHeartyTrainingDS.csv";

        private static string TrainDataPath = GetAbsolutePath(TrainDataRelativePath);

        public MLModel()
        {
            mlContext = new MLContext();
            Train();
        }
        public void Train()
        {
            var trainingDataView = mlContext.Data.LoadFromTextFile<FromHeartyData>(TrainDataPath, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.Concatenate("Features", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalach", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                            .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));
            trainedModel = pipeline.Fit(trainingDataView);
        }
        public FromHeartyPrediction Predict(FromHeartyData input)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<FromHeartyData, FromHeartyPrediction>(trainedModel);
            return predictionEngine.Predict(input);
        }

        private static string GetAbsolutePath(string relativePath)
        {
            String projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(projectDirectory, relativePath);

            return fullPath;
        }
    }
}
