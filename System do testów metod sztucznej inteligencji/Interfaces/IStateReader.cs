using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IStateReader
    {
        bool FilesExist();

        List<SolveInput> GetSolveInputs();

        int GetSolveIteration();

        void DelateFiles();




    }
}
