using System.Reflection;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IDllReader
    {
        public void RunSolve(string dllName, fitnessFunction f, Type fitnessFunctionClass, string fitnessFunctionName, double[,] domain, params double[] parameters);
        public object GetClassObject();
    }
}
