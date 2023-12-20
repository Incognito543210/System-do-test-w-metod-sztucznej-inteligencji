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
        public void RunSolve(string dllName, List<object> testFunctions, double[,] domain, params double[] parameters)
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
                            foreach (var testFunction in testFunctions)
                            {
                                var functionMethod = testFunctions.GetType().GetMethod("FunctionTest");
                                Delegate _delegate = Delegate.CreateDelegate(delegateType, testFunction, functionMethod);
                                method?.Invoke(ClassObject, new object[] { _delegate, domain, parameters });
                            }
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

        public object GetTestFunction(string functionName)
        {
            var filePath = _dllService.GetFunctionDllFile(functionName).DllPath;
            object obj = null;
            var assembly = Assembly.LoadFrom(filePath);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && type.Name == "TestFunction")
                {
                    var instance = Activator.CreateInstance(type);
                    obj = instance;
                }
            }
            return obj;
        }
        public List<object> GetListOfTestFunction(string[] filePaths)
        {
            var list = new List<object>();
            foreach(var filePath in filePaths)
            {
                list.Add(GetTestFunction(filePath));
            }
            return list;
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
