using Microsoft.ML.Data;

namespace FromHeartyAI.DataStructures
{

    public class FromHeartyPrediction
    {
        [ColumnName("PredictionLabel")]
        public bool Prediction;

        public float Probability;

        public float Score;
    }
}
