using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IGeneratePDFReport
    {


        public bool GenerateReport(ICollection<SolveInput> solveInputs, ICollection<SolveOutput> outputs);

    }
}
