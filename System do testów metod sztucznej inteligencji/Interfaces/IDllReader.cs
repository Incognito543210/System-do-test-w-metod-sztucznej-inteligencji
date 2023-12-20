using System.Reflection;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IDllReader
    {
        public void RunSolve(string dllName, List<object> testFunctions, double[,] domain, params double[] parameters);
        public object GetClassObject();
        object GetTestFunction(string functionName);
        List<object> GetListOfTestFunction(string[] filePaths);
    }
}