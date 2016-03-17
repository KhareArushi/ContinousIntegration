using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContinousIntegration.Controllers
{
    public class EmailController : Controller
    {
        /// <summary>
        /// Index method of email controller
        /// </summary>
        /// <returns>Index view</returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
