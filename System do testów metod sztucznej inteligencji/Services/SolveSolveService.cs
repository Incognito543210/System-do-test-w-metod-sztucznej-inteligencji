using System.Xml.Linq;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class SolveSolveService: ISolveService
    {
        private IDllReader _dllReader;
        public SolveSolveService(IDllReader dllReader)
        {
            _dllReader = dllReader;
        }


        public double [] Solve(string algoritmName, string functionName,double[,] domain,params double[] parametres)
        {

            var kasia = _dllReader.GetTestFunction(functionName);
            List<object> listOfFunction = new List<object>();
            listOfFunction.Add(kasia);

        
            _dllReader.RunSolve(algoritmName,listOfFunction,domain,parametres);

            _dllReader.CreateClassObject(algoritmName);
            var optimization = (OptimizationAlgorithm)_dllReader.GetClassObject();

            var result = optimization.XBest;
            return result;
        }

       



    }
}
