using ContinousIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContinousIntegration.Controllers
{
    /// <summary>
    /// Contains all methods to retrive data from database
    /// </summary>
    public class RolesController : Controller
    {
        DBHelper dbHelper = new DBHelper();

        /// <summary>
        /// Method to get all the data about Users,Roles,Projects.
        /// </summary>
        /// <returns></return                
        public ActionResult Index()
        {
            ConfigurationModel configurationDetails = new ConfigurationModel();

            //code for populating users
            configurationDetails.Users = new SelectList(dbHelper.GetAllUsers(), "C_RegisterID", "C_FirstName");

            //Code for populating roles          
            configurationDetails.Roles = new SelectList(dbHelper.GetAllRoles(), "C_RoleID", "C_RoleName");

            //code for populating projects
            configurationDetails.AvailableProjects = new SelectList(dbHelper.GetAllProjects(), "C_ProjectID", "C_ProjectName");
            configurationDetails.AssignedProjects = new SelectList(new[] { "" });

            return View("Index", configurationDetails);
        }

        /// <summary>
        /// Method to save posted data in T_UserProjectMappings table
        /// </summary>
        /// <param name="configurationDetails">configuration details</param>
        /// <returns>Access roles view</returns>  
        [HttpPost]
        public string AccessRoles(ConfigurationModel configurationDetails)
        {
            try
            {
                T_UserProjectMappings userProjectMappings;
                var db = new ContinuousIntegrationEntities();

                //Remove project id from T_UserProjectMappings table
                db.RemoveAllMapProjectIdsOfUser(configurationDetails.SelectedUserID);

                //Remove role id from T_UserRoleMappings table
                db.RemoveAllMapRoleIdsOfUser(configurationDetails.SelectedUserID);

               
                //PREVIOUS CODE
                // if (configurationDetails.ProjectsAssigned != null)
                //{
                //    //Loop to save data in T_UserProjectMappings table
                //    foreach (var item in configurationDetails.ProjectsAssigned)
                //    {
                //        userProjectMappings = new T_UserProjectMappings
                //        {
                //            C_RegisterID = configurationDetails.SelectedUserID,
                //            C_ProjectID = item,
                //            C_LastModified = DateTime.Now
                //        };

                //        db.T_UserProjectMappings.Add(userProjectMappings);
                //        db.SaveChanges();
                //    }
                //}

                if (configurationDetails.SelectedRoleID.Equals(2))
                {
                    //Loop to save data in T_UserProjectMappings table
                    if (configurationDetails.ProjectsAssigned != null)
                    {
                        foreach (var item in configurationDetails.ProjectsAssigned)
                        {
                            userProjectMappings = new T_UserProjectMappings
                            {
                                C_RegisterID = configurationDetails.SelectedUserID,
                                C_ProjectID = item,
                                C_LastModified = DateTime.Now
                            };

                            db.T_UserProjectMappings.Add(userProjectMappings);
                            db.SaveChanges();
                        }
                    }
                }

                else if (configurationDetails.SelectedRoleID.Equals(1))
                {
                    db.RemoveAllMapProjectIdsOfUser(configurationDetails.SelectedUserID);
                }
                dbHelper.SaveUserRoleDetails(configurationDetails);
                return ("true");
            }
            catch (Exception)
            {
                return ("false");
            }
        }

        /// <summary>
        /// Method for changing listboxes data which is called upon onchang event of dropdownlist
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Configuration details</returns>
        public JsonResult GetConfigurationDetails(int userId)
        {
            ConfigurationModel configurationDetails = new ConfigurationModel();
            configurationDetails.Users = new SelectList(dbHelper.GetAllUsers(), "C_RegisterID", "C_FirstName");

            //Code for populating role details        
            configurationDetails.RoleId = (dbHelper.GetRoleId(userId));
            configurationDetails.Roles = new SelectList(dbHelper.GetAllRoles(), "C_RoleID", "C_RoleName", configurationDetails.RoleId);
           
            //Code for populating project details
            configurationDetails.AvailableProjects = new SelectList(dbHelper.GetAvailableProjects(userId), "C_ProjectID", "C_ProjectName");
            configurationDetails.AssignedProjects = new SelectList(dbHelper.GetAssignedProjects(userId), "C_ProjectID", "C_ProjectName");

            //configuration details in json format
            return Json(configurationDetails, JsonRequestBehavior.AllowGet);
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
            //FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "User");
        }
    }
}