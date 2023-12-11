using DAL;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class ParamInfoService : IParamInfoService
    {
        private readonly DataContext _context;

        public ParamInfoService(DataContext context)
        {
            _context = context;
        }

        public ParamInfo GetParamInfo(string name)
        {
            return _context.ParamaInfo.Where(n => n.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }

        public bool ParamInfoExist(string name)
        {
            return _context.ParamaInfo.Any(n => n.Name.Trim().ToLower() == name.Trim().ToLower());
        }
    }
}
