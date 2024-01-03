using Model;
using System.Reflection;
using System_do_testów_metod_sztucznej_inteligencji.Interfaces;

namespace System_do_testów_metod_sztucznej_inteligencji.Services
{
    internal class OptimizationAlgorithm : IOptimizationAlgorithm
    {
        private readonly Type _type;
        private readonly object _algorithm;
        private readonly ParamInfo[] _paramsInfo;
        public OptimizationAlgorithm(object algorithm, Type type)
        {
            _algorithm = algorithm;
            _type = type;
            _paramsInfo = CreateParamInfo();
        }
        private ParamInfo[] CreateParamInfo()
        {
            PropertyInfo paramsInfoProperty = _type.GetProperties().FirstOrDefault(p => p.Name == "ParamsInfo");
            object[] paramsInfoObject = (object[])paramsInfoProperty.GetValue(_algorithm);
            Type? paramInfoType = paramsInfoObject.GetType().GetElementType();

            List<ParamInfo> paramInfos = new List<ParamInfo>();
            foreach (object paramInfoObject in paramsInfoObject)
            {
                var paramInfo = new ParamInfo();

                PropertyInfo nameProperty = paramInfoType.GetProperties().FirstOrDefault(p => p.Name == "Name");
                paramInfo.Name = (string)nameProperty.GetValue(paramInfoObject);

                PropertyInfo descriptionProperty = paramInfoType.GetProperties().FirstOrDefault(p => p.Name == "Description");
                paramInfo.Description = (string)descriptionProperty.GetValue(paramInfoObject);

                PropertyInfo upperBoundaryProperty = paramInfoType.GetProperties().FirstOrDefault(p => p.Name == "UpperBoundary");
                paramInfo.UpperBoundary = (double)upperBoundaryProperty.GetValue(paramInfoObject);

                PropertyInfo stepProperty = paramInfoType.GetProperties().FirstOrDefault(p => p.Name == "Step");
                paramInfo.Step = (double)stepProperty.GetValue(paramInfoObject);

                PropertyInfo lowerBoundaryProperty = paramInfoType.GetProperties().FirstOrDefault(p => p.Name == "LowerBoundary");
                paramInfo.LowerBoundary = (double)lowerBoundaryProperty.GetValue(paramInfoObject);

                paramInfos.Add(paramInfo);
            }

            return paramInfos.ToArray();
        }
        public string Name
        {
            get
            {
                PropertyInfo nameProperty = _type.GetProperties().FirstOrDefault(p => p.Name == "Name");
                string name = (string)nameProperty.GetValue(_algorithm);

                return name;
            }
            set { }
        }
        public ParamInfo[] ParamsInfo
        {
            get => _paramsInfo;
            set { }
        }
        
        public double[] XBest
        {
            get
            {
                PropertyInfo xBestProperty = _type.GetProperties().FirstOrDefault(p => p.Name == "XBest");
                double[] xBestInfo = (double[])xBestProperty.GetValue(_algorithm);

                return xBestInfo;
            }
            set { }
        }
        public double FBest
        {
            get
            {
                PropertyInfo fBestProperty = _type.GetProperties().FirstOrDefault(p => p.Name == "FBest");
                double fBestInfo = (double)fBestProperty.GetValue(_algorithm);

                return fBestInfo; ;
            }
            set { }
        }
        public int NumberOfEvaluationFitnessFunction
        {
            get
            {
                return 0;
            }
            set { }
        }

        public void Solve(fitnessFunction f, double[,] domain, params double[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
