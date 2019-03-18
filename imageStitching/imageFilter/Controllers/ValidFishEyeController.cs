using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imageFilter.Controllers
{
    public class ValidFishEyeController : Controller
    {
        // GET: ValidFishEye
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ValidFishEye()
        {
            return View();
        }
    }
}