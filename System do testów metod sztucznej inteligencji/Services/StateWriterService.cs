using Model;
using Newtonsoft.Json;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class StateWriterService: IStateWriter
    {


       public void  WriteListToFile(ICollection<SolveInput> solveInputs)

        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string fileName = "StateSolveList.json";
            string folderPath = Path.Combine(projectPath, folderName);
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

       public void WriteStateToFile(int iteration)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string fileName = "Iteration.txt";
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

       
    }
    
}
