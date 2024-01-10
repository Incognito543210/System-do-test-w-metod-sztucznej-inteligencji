using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class GenerateCombinations : IGenerateCombinations
    {
       public void GenerateCombinationsRecursively(double[,] parameters, double[] currentCombination, int parameterIndex, List<double[]> combinations)
        {
            if (parameterIndex == parameters.GetLength(0))
            {
                // Add the completed combination to the list
                combinations.Add((double[])currentCombination.Clone());
                return;
            }

            double min = parameters[parameterIndex, 0];
            double max = parameters[parameterIndex, 1];
            double step = parameters[parameterIndex, 2];

            for (double value = min; value <= max; value += step)
            {
                currentCombination[parameterIndex] = value;
                GenerateCombinationsRecursively(parameters, currentCombination, parameterIndex + 1, combinations);
            }
        }

        public List<double[]> GenerateCombinationsFunction(double[,] parameters)
        {
            List<double[]> combinations = new List<double[]>();
            GenerateCombinationsRecursively(parameters, new double[parameters.GetLength(0)], 0, combinations);
            return combinations;
        }



    }
}
