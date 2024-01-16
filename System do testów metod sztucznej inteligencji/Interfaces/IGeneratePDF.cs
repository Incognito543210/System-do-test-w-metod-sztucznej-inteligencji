using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IGeneratePDF
    {
        void GenerateAutoParametersPdfFile();

        bool IsAutoParameters(List<SolveInput> solveInputs);

        void GeneratePdfFile();

    }
}
