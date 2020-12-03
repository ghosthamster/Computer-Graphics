using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ComputerGraphics.Models
{
    public class FractalModel
    {
        public int c { get; set; }
        public int k { get; set; }
        public ColorSchemeType color { get; set; }
        public Complex f(Complex z)
        {
            return System.Numerics.Complex.Pow(z, k) + c;
        }

    }
}
