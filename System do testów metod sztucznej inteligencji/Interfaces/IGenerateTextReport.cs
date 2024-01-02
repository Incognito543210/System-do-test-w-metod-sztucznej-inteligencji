using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IGenerateTextReport
    {
        string ReportString { get;  set; }

         bool GenerateReport(ICollection<SolveInput> solveInputs, ICollection<SolveOutput> outputs);

    }
}
