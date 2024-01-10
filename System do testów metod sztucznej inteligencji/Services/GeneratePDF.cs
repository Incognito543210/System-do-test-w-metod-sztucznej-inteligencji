using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Model;
using Newtonsoft.Json;
namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class GeneratePDF: IGeneratePDF
    {

        public void GeneratePdfFile()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            
            DateTime now = DateTime.Now;
            string currentDateTimeString = now.ToString("yyyy-MM-dd-HH-mm-ss");

            //PDF File path
            string fileNamePDF = $"Raport +{currentDateTimeString}.pdf";
            string folderNamePDF = "PDFRaports";
            string folderPathPDF = Path.Combine(projectPath, folderNamePDF);
            string filePathPDF = Path.Combine(folderPathPDF, fileNamePDF);

            //Soruce for pdf path
            string folderNameState = "StateOfList";
            string fileNameState = "Result.json";
            string folderPathState = Path.Combine(projectPath, folderNameState);
            string filePathState = Path.Combine(folderPathState, fileNameState);

            

            try
            {

                List<PDFModel> existingModels = new List<PDFModel>();

                if (File.Exists(filePathState))
                {
                    string existingJson = File.ReadAllText(filePathState);
                    existingModels = JsonConvert.DeserializeObject<List<PDFModel>>(existingJson);
                }
                existingModels.Sort((a, b) => Math.Abs(a.FittnesFunction).CompareTo(Math.Abs(b.FittnesFunction)));


                    Document document = new Document();
                    PdfWriter.GetInstance(document, new FileStream(filePathPDF, FileMode.Create));
                    document.Open();


                    foreach (var model in existingModels)
                    {

                        document.Add(new Paragraph($"*********************************"));
                        document.Add(new Paragraph($"Algorytm: {model.Algorithm}"));
                        document.Add(new Paragraph($"Funkcja: {model.Function}"));
                        document.Add(new Paragraph($"Min: {string.Join(";", model.Min)}"));
                        document.Add(new Paragraph($"Max: {string.Join(";", model.Max)}"));
                        document.Add(new Paragraph($"Iteracja: {model.Iteration}"));
                        document.Add(new Paragraph($"Populacja: {model.Population}"));
                        document.Add(new Paragraph($"Parametry: {string.Join(";", model.Parameters)}"));
                        document.Add(new Paragraph($"Wartosc funkcji celu: {model.FittnesFunction}"));
                        document.Add(new Paragraph($"Najlepszy osobnik: {string.Join(";", model.BestIndividual)}"));
                        document.Add(new Paragraph($"Liczba wywołań funkcji celu: {model.NumberOfEvaluationFitnessFunction}"));


                }

                    document.Close();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }

            if(File.Exists(filePathState))
            {
                File.Delete(filePathState);
            }



        }

         
    }
}
