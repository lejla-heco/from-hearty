﻿using Microsoft.ML.Data;

namespace FromHeartyAI.DataStructures
{
    public class FromHeartyData
    {
        [LoadColumn(0)]
        public float Cp { get; set; }
        [LoadColumn(1)]
        public float TrestBps { get; set; }
        [LoadColumn(2)]
        public float Chol { get; set; }
        [LoadColumn(3)]
        public float Fbs { get; set; }
        [LoadColumn(4)]
        public float RestEcg { get; set; }
        [LoadColumn(5)]
        public float Thalach { get; set; }
        [LoadColumn(6)]
        public float Exang { get; set; }
        [LoadColumn(7)]
        public float OldPeak { get; set; }
        [LoadColumn(8)]
        public float Slope { get; set; }
        [LoadColumn(9)]
        public float Ca { get; set; }
        [LoadColumn(10)]
        public float Thal { get; set; }
        [LoadColumn(11)]
        public bool Label { get; set; }
    }

    public class FromHeartyOpenAiData : FromHeartyData
    {
        public decimal Percentage { get; set; }
    }
}
