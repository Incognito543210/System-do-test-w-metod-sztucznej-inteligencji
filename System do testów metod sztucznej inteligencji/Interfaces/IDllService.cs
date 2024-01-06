using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IDllService
    {
         bool CreateDllFile(DllFile dllFile);
         bool DllFileExist(string name);
        DllFile GetAlgorithmDllFile(string name);
        DllFile GetFunctionDllFile(string name);
        ICollection<DllFile> GetAlgorithmFiles();
         ICollection<DllFile> GetFunctionFiles();
        bool AlgorithmExists(string algorithm);
        bool AnyAlgorithmExist();
        bool AnyFunctionExist();
        DllFile GetDllFile(string name);
        bool DeleteDLLFile(DllFile dllFile);
    }
}