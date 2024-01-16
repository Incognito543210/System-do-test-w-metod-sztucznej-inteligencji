using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IStateReader
    {
        bool FilesExist();

        List<SolveInput> GetSolveInputs();

        int GetSolveIteration(string fileName);

        List<double[]> ReadCombinationsFromFile();

        void DeleteFiles();
        void DeleteCombinationFiles();
        bool FilesExistCombinations();
        void DeleteResultFile();


    }
}
