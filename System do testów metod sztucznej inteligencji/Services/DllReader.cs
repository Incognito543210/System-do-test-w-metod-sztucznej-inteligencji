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
        public void Run(object targetClass, string testFunction, object[] parameters)
        {
            List<string> list = getFiles();
            ClassObject = GetClassObject();
            foreach (string file in list)
            {
                try
                {
                    Assembly assembly = getAssembly(file);
                    Type[] types = getTypes(assembly);
                    string namespaceName = getNamespaceName(types);
                    foreach (Type type in types)
                    {
                        if (type.GetInterfaces().Any(t => t.Name == "IOptimizationAlgorithm"))
                        {
                            MethodInfo solveMethod = getMethodInfo(type);
                            Delegate @delegate = CreateDelegate(namespaceName, targetClass, testFunction, assembly);
                            object[] _parameters = new object[parameters.Length + 1];
                            _parameters[0] = @delegate;
                            for (int i = 0; i < parameters.Length; i++)
                            {
                                _parameters[i + 1] = parameters[i];
                            }

                            ClassObject = Activator.CreateInstance(type);

                            RunSolve(solveMethod, _parameters);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd: " + ex);
                }
            }
        }
        public object GetClassObject()
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
                    throw new Exception("Błąd: " + ex);
                }
            }
            throw new Exception("Brak klasy dziedziczącej po IOptimizationAlgorithm");
        }
        private Delegate CreateDelegate(string namespaceName, object targetClass, string functionName, Assembly assembly)
        {
            Type delegateType = assembly.GetType(namespaceName + ".fitnessFunction");
            return Delegate.CreateDelegate(delegateType, targetClass, functionName);
        }

        private List<string> getFiles()
        {
            List<string> files = new List<string>();
            ICollection<DllFiles> dllFiles = _dllService.GetFilePaths();
            foreach (DllFiles dllFile in dllFiles)
            {
                files.Add(dllFile.DllPath);
            }
            return files;
        }

        private MethodInfo getMethodInfo(Type type)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            MethodInfo method = methods.FirstOrDefault(m =S> m.Name == "Solve");
            return method;
        }

        private string getNamespaceName(Type[] types)
        {
            return types[0].Namespace;
        }

        private Type[] getTypes(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            return types;
        }

        private void RunSolve(MethodInfo method, object[] parameters)
        {
            method?.Invoke(ClassObject, parameters);
        }

        private Assembly getAssembly(string dllFilePath)
        {
            return Assembly.LoadFrom(dllFilePath);
        }
    }
}
