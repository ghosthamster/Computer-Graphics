using CGv2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;

namespace CGv2.Controllers
{
    public class FractalController : Controller
    {
        // GET: Fractal
        public ActionResult Index()
        {
            //Draw();
            return View();
        }
        /*public ActionResult Index(int c,int k, double zoom)
        {
            Draw(c,k,zoom);
            return View();
        }*/
        [HttpPost]
        public ActionResult print(int c, int k, double zoom, bool color)
        {
            Draw(c,k,zoom,color);
            return View();
        }
        public void Draw()
        {
            Draw(-1, 3, 1.1,false);
        }
        public void Draw(int c, int k, double zoom, bool color)
        {
            var fm = new FractalModel();
            fm.c = c;
            fm.num = k;
            fm.xmax = 2 / zoom;
            fm.xmin = -2 / zoom;
            fm.ymax = 2 / zoom;
            fm.ymin = -2 / zoom;
            fm.color = color;
            var b = fm.image;
            BitmapSource src = (BitmapSource)b.ImageSource;

            string path = @"D:\projekt\Computer-Graphics\CGv2\CGv2\Resourses\images\Fractal.jpg";
            using (FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate))
            {
                BitmapFrame frame = BitmapFrame.Create(src);

                JpegBitmapEncoder enc = new JpegBitmapEncoder();
                enc.Frames.Add(frame);
                enc.Save(fs1);
            }

        }
    }
}