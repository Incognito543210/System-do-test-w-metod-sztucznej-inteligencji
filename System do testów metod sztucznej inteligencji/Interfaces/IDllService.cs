using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IDllService
    {
        public bool AddFolderPath(DllFiles dllFile, string path, string name, string type);
        bool PathExists(string folderPath);
        DllFiles GetAlgorithmDllFile(string name);
        DllFiles GetFunctionDllFile(string name);
        ICollection<DllFiles> GetAlgorithmFilePaths();
        ICollection<DllFiles> GetFunctionFilePaths();
        bool AlgorithmExists(string algorithm);
    }
}