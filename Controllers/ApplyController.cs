using EligibilityTool.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EligibilityTool.Controllers
{
    public class ApplyController : Controller
    {

        private EligibilityToolContext db = new EligibilityToolContext();

        // GET: Apply
        public ActionResult Index()
        {
            return View();
        }
    }
}