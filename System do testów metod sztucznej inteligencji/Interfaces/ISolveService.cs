using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface ISolveService
    {
        (double[], int) Solve(string algoritmName, string functionName, double[,] domain, params double[] parametres);

         ICollection<SolveOutput> LIstOfSolve(ICollection<SolveInput> solveInputs);

        ICollection<SolveInput> Resume();

    }
}
