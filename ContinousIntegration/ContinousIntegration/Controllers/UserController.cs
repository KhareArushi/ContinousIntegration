using ContinousIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;



namespace ContinousIntegration.Controllers
{
    public class UserController : Controller
    {
       
       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(URegisteration reg) //reg will contain the values inserted by user
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Data da = new Data();
                    da.SaveDetails(reg);
                   // ModelState.Clear();
                    return RedirectToAction("List");
                }

                else
                {
                    return View("Index", reg);  //It will return view with  validation msg 

                }             
            }
            catch(Exception e)
            {
                throw new ApplicationException("Plz fill out the data" + e);             
            }
          
            return View(reg);
          }


        public ActionResult List()
        {
            try
            {
                var users = new List<T_Projects>();
                using (CIEntities ci = new CIEntities())
                {
                    users = ci.T_Projects.ToList();
                }
                return View(users);
            }

            catch (Exception e)
            {
                throw new ApplicationException("Error: " + e);
            }
        }


        public ActionResult Login()
        {

            try
            {
                return View();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error: " + e);
            }
        }

      
        public ActionResult GetTreeView(int id)
        {
            try
            {
                Parent parent = new Parent();
                Data dataObj = new Data();

                parent.Project = dataObj.GetallStatus();
                parent.ProjectList = dataObj.GetallProjects();
                parent.Streams = dataObj.GetallStreams();
                parent.Release = dataObj.GetallReleases();
                parent.SubReleases = dataObj.GetallSubReleases();

                return View("GetTreeView", parent);
            }

            catch (Exception e)
            {
                throw new ApplicationException("Error:" + e);
            }

        }

    }
}
