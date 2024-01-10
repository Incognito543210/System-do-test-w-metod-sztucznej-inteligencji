using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IStateWriter
    {
        void WriteListToFile(ICollection<SolveInput> solveInputs);
        void WriteStateIterationToFile(int iteration, string fileName);

        void WriteCombinationsToFile(List<double[]> combinationsList);

        void WriteToJsonFileResult(SolveInput input, SolveOutput output, double[] parameters, int NumofEva);
    }
}
