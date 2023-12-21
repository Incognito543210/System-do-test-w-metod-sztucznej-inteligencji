using DAL;
using Model;
using System.Xml;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class ParamInfoService : IParamInfoService
    {
        private readonly DataContext _context;
        private IDllReader _dllReader;

        public ParamInfoService(DataContext context, IDllReader dllReader)
        {
            _context = context;
            _dllReader = dllReader;
        }

       public ICollection< ParamInfo> GetParamsInfo(string name)
        {
            _dllReader.CreateClassObject(name);
            var optimization = (OptimizationAlgorithm)_dllReader.GetClassObject();
            ParamInfo[] paramInfo = optimization.ParamsInfo;
            return paramInfo;
        }

     





    }
}
