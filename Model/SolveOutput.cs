using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SolveOutput
    {


        public SolveOutput(string nameFunction, double[] solveBest, string nameAlgorithm) { 
        
        this.nameFunction = nameFunction;
            this.solveBest = solveBest; 
            this.nameAlgorithm = nameAlgorithm;
        
        }


        public string nameFunction {  get; set; }
        public double[] solveBest { get; set; }

        public string nameAlgorithm { get; set; }
    }
}
