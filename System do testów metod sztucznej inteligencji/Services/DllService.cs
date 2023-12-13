using DAL;
using Model;
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

        public bool AddFolderPath(DllFiles dllFile, string path)
        {
            if (PathExists(path))
            {
                return false;
            }
            var DllFiles = new DllFiles()
            {
                DllID = dllFile.DllID,
                DllPath = path
            };
            _dataContext.Add(DllFiles);
            return Save();
        }

        public ICollection<DllFiles> GetFilePaths()
        {
            return _dataContext.DllFiles.OrderBy(x => x.DllID).ToList();
        }

        public bool PathExists(string folderPath)
        {
            return _dataContext.DllFiles.Any(p => p.DllPath == folderPath);
        }
        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
