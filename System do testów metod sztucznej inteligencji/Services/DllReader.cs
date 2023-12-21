using Model;
using System.Reflection;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class DllReader : IDllReader
    {
        IDllService _dllService;
        private object ClassObject;
        private Type type;
        private OptimizationAlgorithm optimizationAlgorithm;
        public DllReader(IDllService dllService)
        {
            _dllService = dllService;
        }
        public void RunSolve(string dllName, List<object> testFunctions, double[,] domain, params double[] parameters)
        {
            DllFile dllFile = _dllService.GetAlgorithmDllFile(dllName);
            if (dllFile.DllType == "algorytm")
            {
                CreateClassObject(dllFile.DllName);
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile.DllPath);
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                    MethodInfo method = methods.FirstOrDefault(m => m.Name.ToLower().Trim() == "solve");
                    string namespaceName = type.Namespace;
                    Type delegateType = assembly.GetType(namespaceName + ".fitnessFunction");
                    foreach (var testFunction in testFunctions)
                    {
                        var functionMethod = testFunction.GetType().GetMethod("FunctionTest");
                        Delegate _delegate = Delegate.CreateDelegate(delegateType, testFunction, functionMethod);
                        method?.Invoke(ClassObject, new object[] { _delegate, domain, parameters });
                    
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
                if (type.IsClass && type.Name.ToLower().Trim() == "testfunction")
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
            foreach (var filePath in filePaths)
            {
                list.Add(GetTestFunction(filePath));
            }
            return list;
        }
        public void CreateClassObject(string AlgorithName)
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
                    if (type.GetInterfaces().Any(t => t.Name.ToLower().Trim() == "ioptimizationalgorithm"))
                    {
                        ClassObject = Activator.CreateInstance(type);


                        this.type = type;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Błąd: " + ex);
            }
           
        }
        public object GetClassObject()
        {
            if (ClassObject != null)
            {
                optimizationAlgorithm = new OptimizationAlgorithm(ClassObject, type);
                return optimizationAlgorithm;
            }
            else
            {
                throw new Exception("Obiekt klasy nie istnieje!");
            }
        }
    }
}