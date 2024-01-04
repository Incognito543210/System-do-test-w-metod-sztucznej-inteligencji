using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class StateReaderService : IStateReader
    {
        public bool FilesExist()
        {

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string fileNameIteration = "StateIteration.txt";
            string fileNameList = "StateSolveList.json";
            string folderPath = Path.Combine(projectPath, folderName);
            string filePathIteration = Path.Combine(folderPath, fileNameIteration);
            string filePathList = Path.Combine(folderPath, fileNameList);

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
            string fileName = "StateSolveList.json";
            string folderPath = Path.Combine(projectPath, folderName);
            string filePath = Path.Combine(folderPath, fileName);

            List<SolveInput> solveInputs = null;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                solveInputs =  JsonConvert.DeserializeObject<List<SolveInput>>(json);
                
            }

            return solveInputs;
        }

        public int GetSolveIteration()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string fileName = "StateIteration.txt";
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

        public void DelateFiles()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "StateOfList";
            string fileNameIteration = "StateIteration.txt";
            string fileNameList = "StateSolveList.json";
            string folderPath = Path.Combine(projectPath, folderName);
            string filePathIteration = Path.Combine(folderPath, fileNameIteration);
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
    }
}
