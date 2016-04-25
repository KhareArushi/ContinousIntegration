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
        /// <param name="reg">UserRegisteration model</param>
        /// <returns>Index View</returns>
        [HttpGet]
        public ActionResult Registration()
        {
            return View("Index");
        }

        /// <summary>
        /// This method will save the details of user
        /// </summary>
        /// <param name="register">User details</param>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Create(UserRegisteration register)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBHelper da = new DBHelper();
                    da.SaveUserDetails(register);
                    return RedirectToAction("SendEmail", "Account", register);
                }
                else
                {
                    //this will return view with validation msg 
                    return View("Index", register);
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
        public ActionResult List()
        {
            try
            {
                var projects = new List<T_Projects>();
                T_Projects project;

                using (ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities())
                {
                    //userID is received through querystring 
                    var userId = Convert.ToInt32(Session["LoggedUserID"]);
                    var listOfProjects = ci.GetAllProjects(userId);

                    foreach (var item in listOfProjects) //loop to populate Project with list of projects
                    {
                        project = new T_Projects
                        {
                            C_ProjectID = item.C_ProjectID,
                            C_ProjectName = item.C_ProjectName,
                            C_ProjectDescription = item.C_ProjectDescription,
                            C_LastModified = item.C_LastModified
                        };
                        projects.Add(project);
                    }

                    if (projects.Count == 0)
                    {
                        ViewBag.Msg = "You are not assigned any project.";
                        return View("ErrorPage");
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
        public ActionResult Login(UserLoginModel val)
        {
            try
            {
                var model = val;

                //Validate the user
                ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();
                var user = (from a in ci.T_Registrations
                            where a.C_FirstName == val.C_UserName && a.C_Password == val.C_UserPassword
                            select a).FirstOrDefault();

                
                if (user==null)
                {
                    ModelState.AddModelError("", "Invalid name and password");
                    return View("Login");
                }

                
                Session["LoggedUserID"] = user.C_RegisterID.ToString();
                Session["LoggedUserName"] = user.C_FirstName.ToString();

                return RedirectToAction("List", "User");
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error: " +e);
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
            DBHelper dataObj = new DBHelper();
            ProjectParentModel parentProjectModel;
            T_Status t = new T_Status();


            using (ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities())
            {
                var userId = Convert.ToInt32(Session["LoggedUserID"]);

                //Linq to check user has access to given project or not
                var isUser = (from a in ci.T_UserProjectMappings
                              where a.C_RegisterID == userId && a.C_ProjectID == id
                              select a).FirstOrDefault();

                var isAdmin = (from t1 in ci.T_UserRoleMappings
                               where t1.C_RoleID == 1 && t1.C_RegisterID == userId
                               select t1).FirstOrDefault();

                var doesProjectExists = (from t1 in ci.T_Projects
                                         where t1.C_ProjectID == id
                                         select t1).FirstOrDefault();

                if (doesProjectExists == null)
                {
                    ViewBag.Msg = "This project does not exists!";
                    return View("ErrorPage");
                }
                else if (isAdmin == null && isUser == null)
                {
                    ViewBag.Msg = "You are not assigned this project.";
                    return View("ErrorPage");
                }                
                else
                {
                    var projectDetails = ci.ProjectDetails(userId, id);

                    parentProjectModel = new ProjectParentModel
                    {
                        Releases = new List<T_Releases>(),
                        SubReleases = new List<T_SubReleases>(),
                        ProjectStatus = new List<T_Status>(),
                        Streams = new List<T_Streams>()
                    };
                    parentProjectModel.Statuses = dataObj.GetAllStatuses();

                    foreach (var item in projectDetails) //loop to populate with Pdetails
                    {
                        #region Streams

                        var stream = new T_Streams()
                        {
                            C_StreamName = item.C_StreamName,
                            C_StreamID = item.C_StreamID
                        };
                        parentProjectModel.Streams.Add(stream);

                        #endregion

                        #region Release

                        var release = new T_Releases()
                        {
                            C_ReleaseName = item.C_ReleaseName,
                            C_StreamID = item.C_StreamID,
                            C_ReleaseID = Convert.ToInt32(item.C_ReleaseID)
                        };
                        parentProjectModel.Releases.Add(release);

                        #endregion

                        #region SubRelease

                        T_SubReleases subRelease = new T_SubReleases()
                        {
                            C_SubReleaseName = item.C_SubReleaseName,
                            C_ReleaseID = Convert.ToInt32(item.C_ReleaseID),
                            C_StatusID = Convert.ToInt32(item.C_StatusID)
                        };
                        parentProjectModel.SubReleases.Add(subRelease);

                        #endregion

                        #region ProjectStatus

                        T_Status status = new T_Status()
                        {
                            C_StatusName = item.C_StatusName,
                            C_StatusID = Convert.ToInt32(item.C_StatusID)
                        };
                        parentProjectModel.ProjectStatus.Add(status);

                        #endregion
                    }

                    return View("GetTreeView", parentProjectModel);
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