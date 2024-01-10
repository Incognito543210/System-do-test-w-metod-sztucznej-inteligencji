namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IGenerateCombinations
    {
        List<double[]> GenerateCombinationsFunction(double[,] parameters);

        void GenerateCombinationsRecursively(double[,] parameters, double[] currentCombination, int parameterIndex, List<double[]> combinations);



    }
}
