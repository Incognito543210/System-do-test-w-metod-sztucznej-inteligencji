using Microsoft.EntityFrameworkCore;
using Model;
using System.Reflection;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class DllReader : IDllReader
    {
        IDllService _dllService;
        public DllReader(IDllService dllService)
        {
            _dllService = dllService;
        }
        public void Run(object targetClass, string[] testFunctions, object[] parameters)
        {
            List<string> list = getFiles();
            foreach (string file in list)
            {
                try
                {
                    Assembly assembly = getAssembly(file);
                    Type[] types = getTypes(assembly);
                    string namespaceName = getNamespaceName(types);
                    foreach (Type type in types)
                    {
                        MethodInfo solveMethod = getMethodInfo(type);
                        foreach (string function in testFunctions)
                        {
                            Delegate @delegate = CreateDelegate(namespaceName, targetClass, function, assembly);
                            object[] _parameters = new object[parameters.Length + 1];
                            _parameters[0] = @delegate;
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                _parameters[i+1] = parameters[i];
                            }
                            RunSolve(solveMethod, type, _parameters);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex);
                }
            }
        }
        public object GetParametersInfo()
        {
            List<string> list = getFiles();
            foreach (string file in list)
            {
                try
                {
                    Assembly assembly = getAssembly(file);
                    Type[] types = getTypes(assembly);
                    foreach (Type type in types)
                    {
                        if (type.GetInterfaces().Any(t => t.Name == "IOptimizationAlgorithm"))
                            return Activator.CreateInstance(type);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Błąd!" + ex);
                }
            }
            throw new Exception("Brak klasy dziedziczącej po IOptimizationAlgorithm");
        }
        public Delegate CreateDelegate(string namespaceName, object targetClass, string functionName, Assembly assembly)
        {
            Type delegateType = assembly.GetType(namespaceName + ".fitnessFunction");
            return Delegate.CreateDelegate(delegateType, targetClass, functionName);
        }

        public List<string> getFiles()
        {
            List<string> files = new List<string>();
            ICollection<DllFiles> dllFiles = _dllService.GetFilePaths();
            foreach (DllFiles dllFile in dllFiles)
            {
                files.Add(dllFile.DllPath);
            }
            return files;
        }

        public MethodInfo getMethodInfo(Type type)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            MethodInfo method = methods.FirstOrDefault(m =S> m.Name == "Solve");
            return method;
        }

        public string getNamespaceName(Type[] types)
        {
            return types[0].Namespace;
        }

        public Type[] getTypes(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            return types;
        }

        public void RunSolve(MethodInfo method, Type type, object[] parameters)
        {
            method?.Invoke(Activator.CreateInstance(type), parameters);
        }

        public Assembly getAssembly(string dllFilePath)
        {
            return Assembly.LoadFrom(dllFilePath);
        }
    }
}
