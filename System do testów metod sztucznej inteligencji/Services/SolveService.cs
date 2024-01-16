using System.Xml.Linq;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;
using Model;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    public class SolveService: ISolveService
    {
        private IDllReader _dllReader;
        private IStateWriter _stateWriter;
        private IStateReader _stateReader;
        private IGenerateCombinations _generateCombinations;
        private IGeneratePDF _generatePDF;

        public SolveService(IDllReader dllReader,IStateWriter stateWriter, IStateReader stateReader, IGenerateCombinations generateCombinations, IGeneratePDF generatePDF)
        {
            _dllReader = dllReader;
            _stateWriter = stateWriter;
            _stateReader = stateReader;
            _generateCombinations = generateCombinations;
            _generatePDF = generatePDF;
        }


        public (double [],int)  Solve(string algoritmName, string functionName,double[,] domain,params double[] parametres)
        {

            var kasia = _dllReader.GetTestFunction(functionName);
            List<object> listOfFunction = [kasia];

            _dllReader.RunSolve(algoritmName,listOfFunction,domain,parametres);
            var optimization = (OptimizationAlgorithm)_dllReader.GetClassObject();

            double[] result = new double[domain.GetLength(0)+1];



            result[0] = optimization.FBest;

            for (int i = 1; i<=optimization.XBest.Length; i++)
            {

                result[i] = optimization.XBest[i-1];

            }

            int numOfEva = optimization.NumberOfEvaluationFitnessFunction;
            return (result, numOfEva);
        }

      public ICollection<SolveOutput> LIstOfSolve(ICollection<SolveInput> solveInputs) 
        {
            
            //List of all elements from output save to file with iteration
            List<SolveOutput> resultOfList = new List<SolveOutput>();
            int iteration = 0;
            _stateWriter.WriteStateIterationToFile(iteration, "Iteration.txt");
            _stateWriter.WriteListToFile(solveInputs);

            //Iteration after each element from output
            foreach (var solveInput in solveInputs)
            {
                double[,] parametersFromConvert = JsonConvert.DeserializeObject<double[,]>(solveInput.Parameters);
                double[,] domain = new double[2, solveInput.Min.Length];

                for (int i = 0; i < solveInput.Min.Length; i++)
                {
                    domain[0, i] = solveInput.Min[i];
                    domain[1, i] = solveInput.Max[i];
                }

                //Do all cobinations of parameters from output
                List<double[]> combinationsOfParameters = null;
                //iteration on with parameter calculations are
                int iterationCombination = 0;

                //Read combinations from file 
                if (_stateReader.FilesExistCombinations())
                {
                    combinationsOfParameters = ResumeConbinations();
                    _stateWriter.WriteCombinationsToFile(combinationsOfParameters);
                    _stateWriter.WriteStateIterationToFile(iterationCombination, "IterationCombination.txt");
                }
                else
                {
                    combinationsOfParameters = _generateCombinations.GenerateCombinationsFunction(parametersFromConvert);
                    _stateWriter.WriteCombinationsToFile(combinationsOfParameters);
                    _stateWriter.WriteStateIterationToFile(iterationCombination, "IterationCombination.txt");
                }

                //Iteration on combinations
                foreach (var combination in combinationsOfParameters)
                {
                    var result = Solve(solveInput.Algorithm, solveInput.Function, domain, combination);

                    //Table of the best fittness function, and the best individual
                    double[] resultArray = result.Item1;
                    //NumberOfEvaluationFitnessFunction
                    int resultInteger = result.Item2;

                    var solveOutput = new SolveOutput(solveInput.Function, resultArray, solveInput.Algorithm);

                    //save result to file
                    _stateWriter.WriteToJsonFileResult(solveInput, solveOutput, combination, resultInteger);

                    //add result to list
                    resultOfList.Add(solveOutput);
                    _stateWriter.WriteStateIterationToFile(iterationCombination, "IterationCombination.txt");
                    iterationCombination++;
                }

               
                iteration++;
                _stateWriter.WriteStateIterationToFile(iteration, "Iteration.txt");
                _stateReader.DeleteCombinationFiles();
                
            }

            List<SolveInput>solveInputsList = new List<SolveInput>(solveInputs);
            if (_generatePDF.IsAutoParameters(solveInputsList))
            {
                _generatePDF.GenerateAutoParametersPdfFile();
            }
            else
            {
                _generatePDF.GeneratePdfFile();
            }
            _stateReader.DeleteFiles();

            return resultOfList;
        }


        public ICollection<SolveInput> Resume()
        {
            List<SolveInput> solveInputs = null;
            if (_stateReader.FilesExist())
            {
                int iteration = _stateReader.GetSolveIteration("Iteration.txt");
                solveInputs = _stateReader.GetSolveInputs();

                for (int i = 0; i < iteration; i++)
                {
                    if (solveInputs.Count > 0)
                    {
                        solveInputs.Remove(solveInputs[0]);
                    }
                }
            }

            _stateReader.DeleteFiles();
            return solveInputs;

        }


        public List<double[]> ResumeConbinations()
        {
            List<double[]> combinations = _stateReader.ReadCombinationsFromFile();
            int iteration = _stateReader.GetSolveIteration("IterationCombination.txt");


            for (int i = 0; i < iteration; i++)
            {
                if (combinations.Count > 0)
                {
                    combinations.Remove(combinations[0]);
                }
            }

            _stateReader.DeleteCombinationFiles();
            return combinations;


        }

    }
}
