﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SolveInput
    {
        public string Parameters { get; set; }
        public double[] Min { get; set; }
        public double[] Max { get; set; }

        public string Algorithm { get; set; }
        public string Function { get; set; }

    }
}
