using Model;
using Newtonsoft.Json;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class StateReaderService : IStateReader
    {
        public bool FilesExist()
        {

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string fileNameList = "StateSolveList.json";
            string filePathList = Path.Combine(folderPath, fileNameList);

            string fileNameIteration = "Iteration.txt";
            string filePathIteration = Path.Combine(folderPath, fileNameIteration);
           
            bool exists = false;

            if(File.Exists(filePathIteration) && File.Exists(filePathList))
            {
                exists = true;
            }

            return exists;
        }

        public List<SolveInput> GetSolveInputs()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string fileName = "StateSolveList.json";
            string filePath = Path.Combine(folderPath, fileName);

            List<SolveInput> solveInputs = null;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                solveInputs =  JsonConvert.DeserializeObject<List<SolveInput>>(json);
                
            }

            return solveInputs;
        }

        public int GetSolveIteration(string fileName)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);
            string filePath = Path.Combine(folderPath, fileName);

            int iteration = 0;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {

                     iteration = int.Parse(reader.ReadLine());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd podczas odczytu pliku: {e.Message}");
            }

            return iteration;
        }

        public void DeleteFiles()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string fileNameIteration = "Iteration.txt";
            string filePathIteration = Path.Combine(folderPath, fileNameIteration);

            string fileNameList = "StateSolveList.json";
            string filePathList = Path.Combine(folderPath, fileNameList);

            string fileNameAlgorithState = "State.txt";
            string filePathAlgorithState=Path.Combine(folderPath, fileNameAlgorithState);

            string fileNameResults = "Results.txt";
            string filePathResults = Path.Combine(folderPath, fileNameResults);


            if (File.Exists(filePathIteration))
            {
                File.Delete(filePathIteration);
            }

            if (File.Exists(filePathList))
            {
                File.Delete(filePathList);
            }

            if(File.Exists(filePathAlgorithState))
            {
                File.Delete(filePathAlgorithState);
            }

            if (File.Exists(filePathResults))
            {
                File.Delete(filePathResults);
            }



        }

        public void DeleteCombinationFiles()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string fileNameIteration = "IterationCombination.txt";
            string filePathIteration = Path.Combine(folderPath, fileNameIteration);

            string fileNameList = "Combinations.txt";
            string filePathList = Path.Combine(folderPath, fileNameList);

            if (File.Exists(filePathIteration))
            {
                File.Delete(filePathIteration);
            }
            if (File.Exists(filePathList))
            {
                File.Delete(filePathList);
            }
        }

        public  List<double[]> ReadCombinationsFromFile()
        {
            List<double[]> combinations = new List<double[]>();
            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;

                string folderName = "StateOfList";
                string folderPath = Path.Combine(projectPath, folderName);

                string fileName = "Combinations.txt";
                string filePath = Path.Combine(folderPath, fileName);
           
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        string[] values = line.Split(';').Select(s => s.Trim()).ToArray();
                        double[] combination = new double[values.Length];

                        for (int i = 0; i < values.Length; i++)
                        {
                            if (double.TryParse(values[i], out double parsedValue))
                            {
                                combination[i] = parsedValue;
                            }
                            else
                            {

                                Console.WriteLine($"Conversion error for value: {values[i]}");
                                combination = null;
                                break;
                            }
                        }

                        if (combination != null)
                        {
                            combinations.Add(combination);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd podczas odczytu pliku: {e.Message}");
            }
            return combinations;
        }

        public bool FilesExistCombinations()
        {

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath, folderName);

            string fileNameIteration = "IterationCombination.txt";
            string filePathIteration = Path.Combine(folderPath, fileNameIteration);

            string fileNameList = "Combinations.txt";
            string filePathList = Path.Combine(folderPath, fileNameList);

            bool exists = false;

            if (File.Exists(filePathIteration) && File.Exists(filePathList))
            {
                exists = true;
            }

            return exists;
        }

        public void DeleteResultFile()
        {
            string projectPath2 = AppDomain.CurrentDomain.BaseDirectory;

            string folderName = "StateOfList";
            string folderPath = Path.Combine(projectPath2, folderName);

            string fileName = "Result.json";
            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

        }
    }
}
