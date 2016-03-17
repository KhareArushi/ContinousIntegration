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
    /// <summary>
    /// Controller which manages user
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// This method will return the login page
        /// </summary>
        /// <param name="reg">Users model</param>
        /// <returns>Login View</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        /// <summary>
        /// This method will return registration page for 
        /// registering new user
        /// </summary>
        /// <param name="reg">URegisteration model</param>
        /// <returns>Index View</returns>
        [HttpGet]
        public ActionResult Registration()
        {
            return View("Index");
        }

        /// <summary>
        /// This method will save the details of user
        /// </summary>
        /// <param name="reg">model</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Create(URegisteration reg) //reg will contain the values inserted by user
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Data da = new Data();
                    da.SaveDetails(reg);
                    return RedirectToAction("SendEmail", "Account", reg);
                }
                else
                {
                    return View("Login", reg);  //It will return view with validation msg 
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Plz fill out the data" + e);
            }
        }

        /// <summary>
        /// This method will return the 
        /// gridview of the project Summary
        /// </summary>
        /// <param name="reg">TProjects model</param>
        /// <returns>List view</returns>
        public ActionResult List(int? userID)
        {
            try
            {
                var projects = new List<T_Projects>();
                T_Projects project;
                using (ContinuousIntegrationEntity1 ci = new ContinuousIntegrationEntity1())
                {
                    var listOfProjects = ci.GetAllProjects(userID);

                    foreach (var item in listOfProjects) //loop to populate Projects with list of projects
                    {
                        project = new T_Projects();
                        project.C_ProjectID = item.C_ProjectID;
                        project.C_ProjectName = item.C_ProjectName;
                        project.C_ProjectDescription = item.C_ProjectDescription;
                        project.C_LastModified = item.C_LastModified;
                        projects.Add(project);
                    }
                    return View(projects);
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error: " + e);
            }
        }

        /// <summary>
        /// This method will send an email to notify the user
        /// of his registration
        /// </summary>
        /// <param name="reg"> model</param>
        /// <returns>List view</returns>
        [HttpPost]
        public ActionResult Login(Validation val)
        {
            try
            {
                var model = val;

                //Validate the user
                ContinuousIntegrationEntity1 ci = new ContinuousIntegrationEntity1();
                var user = (from a in ci.T_Registrations 
                            where a.C_FirstName == val.C_UserName && a.C_Password == val.C_UserPassword
                            select a).FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View("Login");
                }

                return RedirectToAction("List", "User", new { userID = user.C_RegisterID });
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error: " + e);
            }
        }

        /// <summary>
        /// This method will display the view having
        /// development status
        /// </summary>
        /// <param name="reg"></param>
        /// <returns>GetTreeView view</returns>
        public ActionResult GetTreeView(int id)
        {
            try
            {
                Data dataObj = new Data();
                Parent parent = new Parent
                {
                    Project = dataObj.GetallStatus(),
                    ProjectList = dataObj.GetallProjects(),
                    Streams = dataObj.GetallStreams(),
                    Release = dataObj.GetallReleases(),
                    SubReleases = dataObj.GetallSubReleases()
                };

                return View("GetTreeView", parent);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error:" + e);
            }
        }
    }
}