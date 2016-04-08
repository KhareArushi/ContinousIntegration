using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    /// <summary>
    /// Helper class for db operations
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// Fetch all the status details
        /// </summary>
        /// <returns>List of status</returns>
        public List<T_Status> GetAllStatuses()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var statusDetails = from status in db.T_Status select status;
                return statusDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch all the projects details
        /// </summary>
        /// <returns>List of projects</returns>
        public List<T_Projects> GetAllProjects()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var projectDetails = from projects in db.T_Projects select projects;
                return projectDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch all the streams details
        /// </summary>
        /// <returns>List of streams</returns>
        public List<T_Streams> GetAllStreams()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var streamsDetails = from streams in db.T_Streams select streams;
                return streamsDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch all the releases details
        /// </summary>
        /// <returns>List of releases</returns>
        public List<T_Releases> GetAllReleases()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var releasesDetails = from releases in db.T_Releases select releases;
                return releasesDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch all the subreleases details
        /// </summary>
        /// <returns>List of sub-releases</returns>
        public List<T_SubReleases> GetAllSubReleases()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var subReleasesDetails = from subReleases in db.T_SubReleases select subReleases;
                return subReleasesDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch all the user deatils
        /// </summary>
        /// <returns>List of users</returns>
        public List<T_Registrations> GetAllUsers()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var userDetails = from registrations in db.T_Registrations select registrations;
                return userDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch all the roles details 
        /// </summary>
        /// <returns>List of Roles</returns>
        public List<T_Roles> GetAllRoles()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var rolesDetails = from roles in db.T_Roles select roles;
                return rolesDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch project details from T_Projects for projects assigned
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List of assigned projects</returns>
        public List<T_Projects> GetAssignedProjects(int userId)
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var projectDetails = from userProjectMappings in db.T_UserProjectMappings
                                     join projects in db.T_Projects on userProjectMappings.C_ProjectID equals projects.C_ProjectID
                                     where userProjectMappings.C_RegisterID == userId
                                     select projects;

                return projectDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch project details for T_Projets for projects available
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List of available projects</returns>
        public List<T_Projects> GetAvailableProjects(int userId)
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var userProjectIds = (from b in db.T_UserProjectMappings where b.C_RegisterID == userId select b.C_ProjectID).ToList();
                var projectDetails = from a in db.T_Projects where !userProjectIds.Contains(a.C_ProjectID) select a;

                return projectDetails.ToList();
            }
        }

        /// <summary>
        /// Fetch the role id of the specific user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List of ids</returns>
        public List<int> GetRoleId(int userId)
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var userRoleId = from userRoleMappings in db.T_UserRoleMappings where userRoleMappings.C_RegisterID == userId select userRoleMappings.C_RoleID;
                return userRoleId.ToList();
            }
        }

        /// <summary>
        /// Save user's details in T_Registrations table
        /// </summary>
        /// <param name="registrationDetails">Details of registering user</param>
        public void SaveUserDetails(UserRegisteration registrationDetails)
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var registrations = new T_Registrations
                {
                    C_FirstName = registrationDetails.C_FirstName,
                    C_LastName = registrationDetails.C_LastName,
                    C_EmailID = registrationDetails.C_EmailID,
                    C_Password = registrationDetails.C_Password
                };

                db.T_Registrations.Add(registrations);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Save user's role details
        /// </summary>
        /// <param name="roleDetails">Role details</param>
        public void SaveUserRoleDetails(ConfigurationModel roleDetails)
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var userRoleMappings = new T_UserRoleMappings
                {
                    C_RegisterID = roleDetails.SelectedUserID,
                    C_RoleID = roleDetails.SelectedRoleID,
                    C_LastModified = DateTime.Now
                };

                db.T_UserRoleMappings.Add(userRoleMappings);
                db.SaveChanges();
            }
        }
    }
}