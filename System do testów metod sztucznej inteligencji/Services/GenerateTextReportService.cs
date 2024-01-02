using Model;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class GenerateTextReportService : IGenerateTextReport
    {
        public string ReportString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly IParamInfoService _paramInfoService ;

       public GenerateTextReportService( IParamInfoService paramInfoService )
        {      
            _paramInfoService = paramInfoService;
        }   

        public bool GenerateReport(ICollection<SolveInput> solveInputs, ICollection<SolveOutput> outputs) 
        {

            string pathToFile = "C:\\Users\\Lenovo\\Desktop\\raport.txt";

            try
            {

                using (StreamWriter sw = new StreamWriter(pathToFile))
                {
                    
                    sw.WriteLine("Raport");


                    
                    for (int i = 0; i < solveInputs.Count; i++)
                    {
                        sw.WriteLine("----------------------------------------------------------");
                        SolveInput solveInput = solveInputs.ElementAt(i);
                        SolveOutput solveOutput = outputs.ElementAt(i);
                      

                        ICollection<ParamInfo> paramsInfo = _paramInfoService.GetParamsInfo(solveInput.Algorithm);

                       
                            sw.WriteLine($"Algorytm: {solveInput.Algorithm}");
                            sw.WriteLine($"Funkcja: {solveInput.Function}");                           
                            sw.WriteLine($"Wartość funkjci celu: {solveOutput.solveBest[0]}");
                            sw.WriteLine("Najlepszy osobnik: ");
                            for(int k=1; k < solveOutput.solveBest.Length; k++)
                            {
                                sw.Write($"{solveOutput.solveBest[k]}, ");
                            }
                                sw.WriteLine();
                          sw.WriteLine($"{paramsInfo.ElementAt(0).Name}: {solveInput.Parameters[0]}");
                        sw.WriteLine($"{paramsInfo.ElementAt(1).Name}: {solveInput.Parameters[1]}");
                        sw.WriteLine($"{paramsInfo.ElementAt(2).Name}: {solveInput.Min.Length}");
                        int p = 2;
                        for (int k=3; k < paramsInfo.Count; k++)
                        {
                            sw.WriteLine($"{paramsInfo.ElementAt(k).Name}: {solveInput.Parameters[p]}");
                            p++;
                        }

                    }
                    sw.WriteLine("----------------------------------------------------------");
                    sw.Close();
                     
                   return true;
                }



            }
            catch (Exception ex) {
                Console.WriteLine($"Wystąpił błąd podczas zapisywania raportu: {ex.Message}");
                return false;
                
            }






        }


    }
}
