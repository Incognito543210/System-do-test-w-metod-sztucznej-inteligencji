using DAL;
using Model;
using System.Xml.Linq;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class DllService : IDllService
    {
        private readonly DataContext _dataContext;
        public DllService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AddFolderPath(DllFiles dllFile, string path, string name, string type)
        {
            if (PathExists(path))
            {
                return false;
            }
            var DllFiles = new DllFiles()
            {
                DllID = dllFile.DllID,
                DllPath = path,
                DllName = name,
                DllType = type
            };
            _dataContext.Add(DllFiles);
            return Save();
        }

        public DllFiles GetAlgorithmDllFile(string name)
        {
            return _dataContext.DllFiles.Where(n => n.DllName == name && n.DllType == "Algorytm").FirstOrDefault();
        }
        public DllFiles GetFunctionDllFile(string name)
        {
            return _dataContext.DllFiles.Where(n => n.DllName == name && n.DllType == "Funkcja").FirstOrDefault();
        }

        public ICollection<DllFiles> GetAlgorithmFilePaths()
        {
            return _dataContext.DllFiles.Where(n => n.DllType == "Algorytm").OrderBy(x => x.DllID).ToList();
        }
        public ICollection<DllFiles> GetFunctionFilePaths()
        {
            return _dataContext.DllFiles.Where(n => n.DllType == "Funkcja").OrderBy(x => x.DllID).ToList();
        }

        public bool PathExists(string filePath)
        {
            return _dataContext.DllFiles.Any(p => p.DllPath == filePath);
        }
        public bool AlgorithmExists(string algorithm)
        {
            return _dataContext.DllFiles.Any(n => n.DllName == algorithm && n.DllType == "Algorytm");
        }
        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
