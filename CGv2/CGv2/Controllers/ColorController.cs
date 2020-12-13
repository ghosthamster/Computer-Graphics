using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    [Route("ColorModelM")]
    public class ColorModelMController : Controller
    {
        // GET: ColorModelM
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("convert")]
        public ActionResult convert(HttpPostedFileBase postedFile)
        {
            string filePath = "";
            filePath = Server.MapPath("~/Resourses/images/");
            if (postedFile == null)
            {
                postedFile = Request.Files["userFile"];
            }
            filePath = filePath +"rgb.jpg";
            postedFile.SaveAs(filePath);
            var cm = new ColorModelM();
            cm.convertImg();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("changebrightnes")]
        public ActionResult changebrightnes(int brightnes,string color)
        {
            var cm = new ColorModelM();
            cm.changebrightnes(brightnes,color);
            return View();
        }
        [HttpPost]
        [Route("saveimg")]
        public ActionResult saveimg(string path)
        {
            //add code
            return View();
        }
        [HttpPost]
        [Route("pixel")]
        public point pixel(int r, int g, int b)
        {
            //add code
            double h, s, l;
            ColorModelM.RgbToHls(r, g, b, out h, out l, out s);
            var p = new point() { h = h, l = l, s = s };
            return p;
        }
        public class point
        {
            public double h { get; set; }
            public double l { get; set; }
            public double s { get; set; }
        }
    }
}