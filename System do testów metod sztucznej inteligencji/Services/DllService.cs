using DAL;
using Model;
using System.Security.Cryptography;
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

        public bool CreateDllFile(DllFile dllFile)
        {
            _dataContext.Add(dllFile);
            return Save();
        }

        public DllFile GetAlgorithmDllFile(string name)
        {
            return _dataContext.DllFiles.Where(n => n.DllName.ToLower().Trim() == name.ToLower().Trim() && n.DllType.ToLower().Trim() == "algorytm").FirstOrDefault();
        }
        public DllFile GetFunctionDllFile(string name)
        {
            return _dataContext.DllFiles.Where(n => n.DllName.ToLower().Trim() == name.ToLower().Trim() && n.DllType.ToLower().Trim() == "funkcja").FirstOrDefault();
        }

        public ICollection<DllFile> GetAlgorithmFiles()
        {
            return _dataContext.DllFiles.Where(n => n.DllType.ToLower().Trim() == "algorytm").OrderBy(x => x.DllID).ToList();
        }
        public ICollection<DllFile> GetFunctionFiles()
        {
            return _dataContext.DllFiles.Where(n => n.DllType.ToLower().Trim() == "funkcja").OrderBy(x => x.DllID).ToList();
        }

        public bool DllFileExist(string name)
        {
            return _dataContext.DllFiles.Any(p => p.DllName.ToLower().Trim() == name.ToLower().Trim()); 
        }
        public bool AlgorithmExists(string algorithm)
        {
            return _dataContext.DllFiles.Any(n => n.DllName.Trim().ToLower() == algorithm.Trim().ToLower() && n.DllType.Trim().ToLower() == "algorytm");
        }
        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool AnyAlgorithmExist()
        {
            return _dataContext.DllFiles.Any(n =>  n.DllType.Trim().ToLower() == "algorytm");
        }

        public bool AnyFunctionExist()
        {
            return _dataContext.DllFiles.Any(n => n.DllType.Trim().ToLower() == "funkcja");
        }
    }
}