using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private IDllReader _dllReader;
        public AlgorithmService(IDllReader dllReader) 
        {
            _dllReader = dllReader;
        }


        
    }
}
