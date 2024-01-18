using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
