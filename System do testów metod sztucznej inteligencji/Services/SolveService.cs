using System.Xml.Linq;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class SolveService: ISolveService
    {
        private IDllReader _dllReader;
        public SolveService(IDllReader dllReader)
        {
            _dllReader = dllReader;
        }


        public double [] Solve(string algoritmName, string functionName,double[,] domain,params double[] parametres)
        {

            var kasia = _dllReader.GetTestFunction(functionName);
            List<object> listOfFunction = new List<object>();
            listOfFunction.Add(kasia);

        

            _dllReader.RunSolve(algoritmName,listOfFunction,domain,parametres);
            var optimization = (OptimizationAlgorithm)_dllReader.GetClassObject();

            double[] result = new double[domain.GetLength(0)+1];



            result[0] = optimization.FBest;

            for (int i = 1; i<=optimization.XBest.Length; i++)
            {

                result[i] = optimization.XBest[i-1];

            }
            
            return result;
        }

      public ICollection<SolveOutput> LIstOfSolve(ICollection<SolveInput> solveInputs) 
        {


            List<SolveOutput> resultOfList = new List<SolveOutput>();
            foreach (var solveInput in solveInputs)
            {
                double[,] domain = new double[2, solveInput.Min.Length];

                for (int i = 0; i < solveInput.Min.Length; i++)
                {
                    domain[0, i] = solveInput.Min[i];
                    domain[1, i] = solveInput.Max[i];
                }
                var result = Solve(solveInput.Algorithm,solveInput.Function,domain,solveInput.Parameters);

                var solveOutput = new SolveOutput(solveInput.Function, result, solveInput.Algorithm);

                resultOfList.Add(solveOutput);

            }

            return resultOfList;
        
        }



    }
}
