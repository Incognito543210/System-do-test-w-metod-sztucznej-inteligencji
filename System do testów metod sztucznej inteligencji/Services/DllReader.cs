using Model;
using System.Reflection;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class DllReader : IDllReader
    {
        IDllService _dllService;
        public object ClassObject;
        public DllReader(IDllService dllService)
        {
            _dllService = dllService;
        }
        public void RunSolve(string dllName, fitnessFunction f, Type fitnessFunctionClass, string fitnessFunctionName, double[,] domain, params double[] parameters)
        {
            DllFiles dllFile = _dllService.GetAlgorithmDllFile(dllName);
            if (dllFile.DllType == "Algorytm")
            {
                CreateClassObject(dllFile.DllPath);
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile.DllPath);
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (type.GetInterfaces().Any(t => t.Name == "IOptimizationAlgorithm"))
                        {
                            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                            MethodInfo method = methods.FirstOrDefault(m => m.Name == "Solve");
                            string namespaceName = type.Namespace;
                            Type delegateType = assembly.GetType(namespaceName + ".fitnessFunction");
                            Delegate _delegate = Delegate.CreateDelegate(delegateType, fitnessFunctionClass, fitnessFunctionName);
                            object obj = Activator.CreateInstance(type);
                            method?.Invoke(obj, new object[] { _delegate, domain, parameters });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd: " + ex);
                }
            }
            else
            {
                throw new Exception("Podany plik dll nie jest algorytmem!");
            }
        }
        private void CreateClassObject(string AlgorithName)
        {
            string dllPath;
            if (_dllService.AlgorithmExists(AlgorithName))
            {
                dllPath = _dllService.GetAlgorithmDllFile(AlgorithName).DllPath;
            }
            else
            {
                throw new Exception("Taki algorytm nie istnieje!");
            }
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllPath);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterfaces().Any(t => t.Name == "IOptimizationAlgorithm"))
                        ClassObject = Activator.CreateInstance(type);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd: " + ex);
            }
            throw new Exception("Brak klasy dziedziczącej po IOptimizationAlgorithm");
        }
        public object GetClassObject()
        {
            if(ClassObject != null)
                return ClassObject;
            else
            {
                throw new Exception("Obiekt klasy nie istnieje!");
            }
        }
    }
}
