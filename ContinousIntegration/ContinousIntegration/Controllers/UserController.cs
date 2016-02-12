using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContinousIntegration.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult List()
        {
            var users = new List<T_Projects>();
            using (CIEntities ci = new CIEntities())
            {
                users = ci.T_Projects.ToList();
            }
            return View(users);
        }

    }
}
