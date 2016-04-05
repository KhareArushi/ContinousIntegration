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
        public ActionResult List() //int? userID is for receiving id through querystring 
        {
            try
            {
                var projects = new List<T_Projects>();
                T_Projects project;
                using (ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities())
                {
                    var UID = Convert.ToInt32(Session["LoggedUserID"]);
                    var listOfProjects = ci.GetAllProjects(UID);

                    foreach (var item in listOfProjects) //loop to populate Project with list of projects
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
                ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();
                var user = (from a in ci.T_Registrations
                            where a.C_FirstName == val.C_UserName && a.C_Password == val.C_UserPassword
                            select a).FirstOrDefault();

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View("Login");
                }
                Session["LoggedUserID"] = user.C_RegisterID.ToString();
                Session["LoggedUserName"] = user.C_FirstName.ToString();
                //TempData["UserID"] = user.C_RegisterID;
                return RedirectToAction("List", "User");

                //return RedirectToAction("List", "User", new { userID = user.C_RegisterID });
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
        /// <param name="id">Project Id</param>
        /// <returns>GetTreeView view</returns>
        public ActionResult GetTreeView(int id)
        {
            Data dataObj = new Data();
            Parent parent = new Parent();
            T_Status t = new T_Status();

            parent.Statuses = dataObj.GetallStatus();

            using (ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities())
            {
                var UID = Convert.ToInt32(Session["LoggedUserID"]);

                //linq to check user has access to given project or not
                var isUser = (from a in ci.T_UserProjectMappings
                              where a.C_RegisterID == UID && a.C_ProjectID == id
                              select a).FirstOrDefault();

                var isAdmin = (from t1 in ci.T_UserRoleMappings
                               where t1.C_RoleID == 1 && t1.C_RegisterID == UID
                               select t1).FirstOrDefault();

                var doesProjectExists = (from t1 in ci.T_Projects
                                         where t1.C_ProjectID == id
                                         select t1).FirstOrDefault();

                if (doesProjectExists == null)
                {
                    ViewBag.Msg = "This project doesnot exists!";
                    return View("ErrorPage");
                }
                else if (isAdmin == null && isUser == null)
                {
                    ViewBag.Msg = "You are not assigned this project.";
                    return View("ErrorPage");
                }
                else
                {
                    var projectDetails = ci.ProjectDetails(UID, id);

                    parent.Release = new List<T_Releases>();
                    parent.SubReleases = new List<T_SubReleases>();
                    parent.Project = new List<T_Status>();
                    parent.Streams = new List<T_Streams>();

                    foreach (var item in projectDetails) //loop to populate with Pdetails
                    {
                        #region Streams

                        var stream = new T_Streams()
                        {
                            C_StreamName = item.C_StreamName,
                            C_StreamID = item.C_StreamID
                        };
                        parent.Streams.Add(stream);

                        #endregion

                        #region Release

                        var release = new T_Releases()
                        {
                            C_ReleaseName = item.C_ReleaseName,
                            C_StreamID = item.C_StreamID,
                            C_ReleaseID = Convert.ToInt32(item.C_ReleaseID)
                        };
                        parent.Release.Add(release);

                        #endregion

                        #region SubRelease

                        T_SubReleases subRelease = new T_SubReleases()
                        {
                            C_SubReleaseName = item.C_SubReleaseName,
                            C_ReleaseID = Convert.ToInt32(item.C_ReleaseID),
                            C_StatusID = Convert.ToInt32(item.C_StatusID)
                        };
                        parent.SubReleases.Add(subRelease);

                        #endregion

                        #region ProjectStatus

                        T_Status status = new T_Status()
                        {
                            C_StatusName = item.C_StatusName,
                            C_StatusID = Convert.ToInt32(item.C_StatusID)
                        };
                        parent.Project.Add(status);

                        #endregion
                    }

                    return View("GetTreeView", parent);
                }
            }
        }

        /// <summary>
        /// Logs out the user
        /// </summary>
        /// <returns>To the login pages</returns>
        public ActionResult Logout()
        {
            //Disable back button In all browsers.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "User");
        }
    }
}
