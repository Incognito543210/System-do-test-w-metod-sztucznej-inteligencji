using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IStateWriter
    {
        void WriteListToFile(ICollection<SolveInput> solveInputs);
        void WriteStateToFile(int iteration);

        
    }
}
