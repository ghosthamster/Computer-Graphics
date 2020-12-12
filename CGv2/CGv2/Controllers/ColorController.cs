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
            //Draw();
            return View();
        }
        /*public ActionResult Index(int c,int k, double zoom)
        {
            Draw(c,k,zoom);
            return View();
        }*/

    }
}