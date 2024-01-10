using Model;
using Newtonsoft.Json;
using System.Transactions;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class StateWriterService: IStateWriter
    {


       public void  WriteListToFile(ICollection<SolveInput> solveInputs)

        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string fileName = "StateSolveList.json";
            string filePath = Path.Combine(folderPath, fileName);

            try
            {

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                if (File.Exists(filePath))
                {
                    Console.WriteLine("Plik już istnieje.");
                }
                else
                {
                    string json = JsonConvert.SerializeObject(solveInputs, Formatting.Indented);
                    File.WriteAllText(filePath, json);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }

        

       public void WriteStateIterationToFile(int iteration,string fileName )
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string filePath = Path.Combine(folderPath, fileName);
      
            try
            {
                
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {

                        writer.Write($"{iteration}");
                    }
                
            }
            catch (Exception e) { Console.WriteLine($"Wystąpił błąd podczas zapisu do pliku: {e.Message}"); }

        }



        public void WriteCombinationsToFile(List<double[]> combinationsList )
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);
            
            string fileName = "Combinations.txt";
            string filePath = Path.Combine(folderPath, fileName);

            try
            {

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    int i = 0;
                    foreach (var combinations in combinationsList)
                    {
                
                            sw.Write(string.Join("; ", combinations));
                        Console.WriteLine(i++);
                        sw.WriteLine();
                    }
                }


            }
            catch (Exception e) { Console.WriteLine($"Wystąpił błąd podczas zapisu do pliku: {e.Message}"); }
        }

       
        public void WriteToJsonFileResult(SolveInput input, SolveOutput output, double[] parameters, int NumofEva)
        {
           
            string projectPath2 = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath2, folderName);

            string fileName = "Result.json";
            string filePath = Path.Combine(folderPath, fileName);



            double[] onlyParameters = parameters.Skip(2).ToArray();
            double bestFunction = output.solveBest.First();
            double[] bestIndividual = output.solveBest.Skip(1).ToArray();

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                PDFModel pDFModel = new PDFModel(
                    input.Algorithm,
                    input.Function,
                    input.Min,
                    input.Max,
                    parameters[0],
                    parameters[1],
                    onlyParameters,
                    bestFunction,
                    bestIndividual,
                    NumofEva
                );

                List<PDFModel> existingModels = new List<PDFModel>();

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    existingModels = JsonConvert.DeserializeObject<List<PDFModel>>(existingJson);
                }

                existingModels.Add(pDFModel);

                string updatedJson = JsonConvert.SerializeObject(existingModels, Formatting.Indented);

                File.WriteAllText(filePath, updatedJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }

        }


    }
    
}
