using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public delegate double fitnessFunction(params double[] arg);
    interface IOptimizationAlgorithm
    {
        string Name { get; set; }
        void Solve(fitnessFunction f, double[, ] domain, params double[] parameters);
        ParamInfo[] ParamsInfo { get; set; }
        double[] XBest { get; set; }
        double FBest { get; set; }
        int NumberOfEvaluationFitnessFunction { get; set; }





    }
}
