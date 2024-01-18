using FromHeartyAI.ML_Model;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
