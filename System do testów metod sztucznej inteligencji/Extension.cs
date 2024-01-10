using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using System_do_testów_metod_sztucznej_inteligencji.Services;

namespace System_do_testów_metod_sztucznej_inteligencji
{
     public static class Extension
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {

            services.AddScoped<IParamInfoService, ParamInfoService>();
            services.AddScoped<IDllService, DllService>();
            services.AddScoped<IDllReader, DllReader>();
            services.AddScoped<ISolveService, SolveService>();
            services.AddScoped<IStateReader, StateReaderService>();
            services.AddScoped<IStateWriter, StateWriterService>();
            services.AddScoped<IGenerateCombinations, GenerateCombinations>();
            services.AddScoped<IGeneratePDF, GeneratePDF>();
            return services;

        }



    }
}
