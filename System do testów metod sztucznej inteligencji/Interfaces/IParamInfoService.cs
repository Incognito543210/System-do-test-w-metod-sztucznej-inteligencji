using Model;

namespace System_do_testów_metod_sztucznej_inteligencji.Interfaces
{
    public interface IParamInfoService
    {
        public ParamInfo GetParamInfo(string name);
        public bool ParamInfoExist(string name);


    }
}
