using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Media;

namespace CGv2.Models
{
    public class FractalModel
    {
        public int c { get; set; }
        public int num { get; set; }
        public double xmin { get; set; }
        public double xmax { get; set; }
        public double ymin { get; set; }
        public double ymax { get; set; }
        public bool color { get; set; }
        public ImageBrush image { get {
                return new Fractal(c, num, xmin, xmax, ymin, ymax, color).draw();
            } }
    }
}