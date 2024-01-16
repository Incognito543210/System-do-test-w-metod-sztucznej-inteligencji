using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Model;
using Newtonsoft.Json;
namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class GeneratePDF: IGeneratePDF
    {


        public bool IsAutoParameters(List<SolveInput> solveInputs)
        {
            string firstNameofAlgorithm = solveInputs.First().Algorithm.Trim().ToLower();
            string secondNameofAlgorithm = solveInputs[1].Algorithm.Trim().ToLower();

            if(string.Equals(firstNameofAlgorithm,secondNameofAlgorithm))
                {
                    return false;
                }

            return true;

        }


        public void GenerateAutoParametersPdfFile()
        {

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;


            DateTime now = DateTime.Now;
            string currentDateTimeString = now.ToString("yyyy-MM-dd-HH-mm-ss");

            //PDF File path
            string fileNamePDF = $"Raport Auto Parameters +{currentDateTimeString}.pdf";
            string folderNamePDF = "PDFRaports";
            string folderPathPDF = Path.Combine(projectPath, folderNamePDF);
            string filePathPDF = Path.Combine(folderPathPDF, fileNamePDF);

            //Soruce for pdf path
            string folderNameState = "StateOfList";
            string fileNameState = "Result.json";
            string folderPathState = Path.Combine(projectPath, folderNameState);
            string filePathState = Path.Combine(folderPathState, fileNameState);

            List<PDFModel> existingModels = new List<PDFModel>();

            if (File.Exists(filePathState))
            {
                string existingJson = File.ReadAllText(filePathState);
                existingModels = JsonConvert.DeserializeObject<List<PDFModel>>(existingJson);
            }

            var groupedModels = existingModels.GroupBy(model => new { model.Algorithm, model.Function, model.Iteration, model.Population });

            var bestModels = groupedModels.Select(group =>
            group.OrderBy(model => model.FittnesFunction).FirstOrDefault()).ToList();

            bestModels.Sort((a, b) => Math.Abs(a.FittnesFunction).CompareTo(Math.Abs(b.FittnesFunction)));

            var algorithmAdded = new Dictionary<string, bool>();

            var theBestParameters = new List<PDFModel>();

            foreach(var model in bestModels)
            {
                if (!algorithmAdded.ContainsKey(model.Algorithm))
                {
                    theBestParameters.Add(model);
                    algorithmAdded[model.Algorithm]= true;
                }
            }
            try
            {

                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(filePathPDF, FileMode.Create));
                document.Open();
                
                document.Add(new Paragraph($"Najlepsze parametry,populacja i iteracja algorytmów dla funkcji {bestModels[1].Function}"));

                foreach (var model in theBestParameters)
                {

                    document.Add(new Paragraph($"********************************************************************************"));
                    document.Add(new Paragraph($"Algorytm: {model.Algorithm}"));
                    document.Add(new Paragraph($"Funkcja: {model.Function}"));
                    document.Add(new Paragraph($"Dziedzina:"));
                    document.Add(new Paragraph($"- Min: {string.Join(";", model.Min)}"));
                    document.Add(new Paragraph($"- Max: {string.Join(";", model.Max)}"));
                    document.Add(new Paragraph($"Iteracja: {model.Iteration}"));
                    document.Add(new Paragraph($"Populacja: {model.Population}"));
                    document.Add(new Paragraph($"Parametry: {string.Join(";", model.Parameters)}"));
                    document.Add(new Paragraph($"Wartosc funkcji celu: {model.FittnesFunction}"));
                    document.Add(new Paragraph($"Najlepszy osobnik: {string.Join(";", model.BestIndividual)}"));
                    document.Add(new Paragraph($"Liczba wywołań funkcji celu: {model.NumberOfEvaluationFitnessFunction}"));


                }
                document.Add(new Paragraph($"********************************************************************************"));
                document.Add(new Paragraph($"Najlepsze parametry dla funkcji {bestModels[1].Function} "));

                foreach (var model in bestModels)
                {

                    document.Add(new Paragraph($"*********************************"));
                    document.Add(new Paragraph($"Algorytm: {model.Algorithm}"));
                    document.Add(new Paragraph($"Funkcja: {model.Function}"));
                    document.Add(new Paragraph($"Dziedzina:"));
                    document.Add(new Paragraph($"- Min: {string.Join(";", model.Min)}"));
                    document.Add(new Paragraph($"- Max: {string.Join(";", model.Max)}"));
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

            if (File.Exists(filePathState))
            {
                File.Delete(filePathState);
            }



        }



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
                        document.Add(new Paragraph($"Dziedzina:"));
                        document.Add(new Paragraph($"- Min: {string.Join(";", model.Min)}"));
                        document.Add(new Paragraph($"- Max: {string.Join(";", model.Max)}"));
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
