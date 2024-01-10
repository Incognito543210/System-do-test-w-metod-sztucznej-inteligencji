using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PDFModel
    {
        public string Algorithm {  get; set; }
        public string Function { get; set; }
        public double[] Min {  get; set; }
        public double[] Max { get; set; }
        public double Iteration { get; set; }
        public double Population { get; set; }
        public double[] Parameters { get; set; }
        public double FittnesFunction { get; set; }
        public double[] BestIndividual { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }

       public PDFModel(string Algorithm, string Function, double[] Min, double[] Max,double Iteration, double Population, double[] Parameters, double FittnesFunction, double[] BestIndividual, int NumberOfEvaluationFitnessFunction) 
        { 
            this.Algorithm = Algorithm;
            this.Function = Function;
            this.Min = Min;
            this.Max = Max; 
            this.Iteration = Iteration;
            this.Population = Population;
            this.Parameters = Parameters;
            this.FittnesFunction = FittnesFunction;
            this.BestIndividual = BestIndividual;
            this.NumberOfEvaluationFitnessFunction = NumberOfEvaluationFitnessFunction;
        
        }


    }
}
