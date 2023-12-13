using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IDllService
    {
        ICollection<DllFiles> GetFilePaths();
        public bool AddFolderPath(DllFiles dllFile, string path);
        bool PathExists(string folderPath);
    }
}
