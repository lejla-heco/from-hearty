// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace HeartyBase.ConsoleApp
{
    public partial class HeartyBase
    {
        /// <summary>
        /// model input class for HeartyBase.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(0)]
            [ColumnName(@"HeartDisease")]
            public bool HeartDisease { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"BMI")]
            public float BMI { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"Smoking")]
            public bool Smoking { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"AlcoholDrinking")]
            public string AlcoholDrinking { get; set; }

            [LoadColumn(4)]
            [ColumnName(@"Stroke")]
            public bool Stroke { get; set; }

            [LoadColumn(5)]
            [ColumnName(@"PhysicalHealth")]
            public float PhysicalHealth { get; set; }

            [LoadColumn(6)]
            [ColumnName(@"MentalHealth")]
            public float MentalHealth { get; set; }

            [LoadColumn(7)]
            [ColumnName(@"DiffWalking")]
            public bool DiffWalking { get; set; }

            [LoadColumn(8)]
            [ColumnName(@"Sex")]
            public string Sex { get; set; }

            [LoadColumn(9)]
            [ColumnName(@"AgeCategory")]
            public string AgeCategory { get; set; }

            [LoadColumn(10)]
            [ColumnName(@"Diabetic")]
            public bool Diabetic { get; set; }

            [LoadColumn(11)]
            [ColumnName(@"PhysicalActivity")]
            public bool PhysicalActivity { get; set; }

            [LoadColumn(12)]
            [ColumnName(@"GenHealth")]
            public string GenHealth { get; set; }

            [LoadColumn(13)]
            [ColumnName(@"SleepTime")]
            public float SleepTime { get; set; }

            [LoadColumn(14)]
            [ColumnName(@"Asthma")]
            public bool Asthma { get; set; }

            [LoadColumn(15)]
            [ColumnName(@"KidneyDisease")]
            public bool KidneyDisease { get; set; }

            [LoadColumn(16)]
            [ColumnName(@"SkinCancer")]
            public bool SkinCancer { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for HeartyBase.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"HeartDisease")]
            public bool HeartDisease { get; set; }

            [ColumnName(@"BMI")]
            public float BMI { get; set; }

            [ColumnName(@"Smoking")]
            public float[] Smoking { get; set; }

            [ColumnName(@"AlcoholDrinking")]
            public float[] AlcoholDrinking { get; set; }

            [ColumnName(@"Stroke")]
            public float[] Stroke { get; set; }

            [ColumnName(@"PhysicalHealth")]
            public float PhysicalHealth { get; set; }

            [ColumnName(@"MentalHealth")]
            public float MentalHealth { get; set; }

            [ColumnName(@"DiffWalking")]
            public float[] DiffWalking { get; set; }

            [ColumnName(@"Sex")]
            public float[] Sex { get; set; }

            [ColumnName(@"AgeCategory")]
            public float[] AgeCategory { get; set; }

            [ColumnName(@"Diabetic")]
            public float[] Diabetic { get; set; }

            [ColumnName(@"PhysicalActivity")]
            public float[] PhysicalActivity { get; set; }

            [ColumnName(@"GenHealth")]
            public float[] GenHealth { get; set; }

            [ColumnName(@"SleepTime")]
            public float SleepTime { get; set; }

            [ColumnName(@"Asthma")]
            public float[] Asthma { get; set; }

            [ColumnName(@"KidneyDisease")]
            public float[] KidneyDisease { get; set; }

            [ColumnName(@"SkinCancer")]
            public float[] SkinCancer { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public bool PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float Score { get; set; }

            [ColumnName(@"Probability")]
            public float Probability { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("HeartyBase.mlnet");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

    }
}
