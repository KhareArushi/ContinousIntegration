using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContinousIntegration.Controllers
{
    /// <summary>
    /// Parent controller of all the projects
    /// </summary>
    public class ParentController : Controller
    {
        /// <summary>
        /// This method will generate tree view of the projects
        /// and its details
        /// </summary>
        /// <returns>Treeview of all the projects</returns>
        public ActionResult TreeView()
        {
            ContinousIntegration.Models.Data pqr = new ContinousIntegration.Models.Data();
            ContinousIntegration.Models.Parent abc = new ContinousIntegration.Models.Parent();

            abc.Project = pqr.GetallStatus();
            abc.ProjectList = pqr.GetallProjects();
            abc.Streams = pqr.GetallStreams();
            abc.Release = pqr.GetallReleases();
            abc.SubReleases = pqr.GetallSubReleases();
            return View(abc);
        }

    }
}
