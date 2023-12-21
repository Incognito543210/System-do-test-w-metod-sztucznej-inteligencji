using DAL;
using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class ParamInfoService : IParamInfoService
    {
        private readonly DataContext _context;
        private DllReader _dllReader;

        public ParamInfoService(DataContext context, DllReader dllReader)
        {
            _context = context;
            _dllReader = dllReader;
        }

       // public ParamInfo GetParamsInfo(string name)
      //  {
       //     _dllReader.CreateClassObject("HarrisHawks");

        //    var algorithmObject = _dllReader.GetClassObject;

//_dllReader.ClassObject = algorithmObject;

      //      algorithmObject.
            

      //  }

     





    }
}
