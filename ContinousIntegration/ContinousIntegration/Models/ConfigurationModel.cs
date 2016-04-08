using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ContinousIntegration.Models
{
    /// <summary>
    /// Class for configuration model
    /// </summary>
    public class ConfigurationModel
    {
        public int SelectedUserID { get; set; }
        public List<int> ProjectsAvailable { get; set; }
        public List<int> ProjectsAssigned { get; set; }
        public int SelectedRoleID { get; set; }

        //Contains user name
        public SelectList Users { get; set; }

        [ScriptIgnore]
        public List<T_Roles> Roles { get; set; }
        public List<int> RoleId { get; set; }

        //Contain project name
        public SelectList AvailableProjects { get; set; }
        public SelectList AssignedProjects { get; set; }
    }
}