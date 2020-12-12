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
        public ActionResult convert(string FilePath)
        {
            //add code
            var cm = new ColorModelM();
            cm.convertImg(FilePath);
            return View();
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
        [HttpGet]
        [Route("pixel")]
        public ActionResult pixel(int x,int y)
        {
            //add code
            var cm = new ColorModelM();
            //cm.convertImg(FilePath);
            return View();
        }
    }
}