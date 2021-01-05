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
    [Route("Affin")]
    public class AffinController : Controller
    {
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
