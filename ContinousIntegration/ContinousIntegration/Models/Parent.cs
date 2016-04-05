using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    /// <summary>
    /// Contains all the models
    /// </summary>
    public class Parent
    {
        /// <summary>
        /// Model containing the status
        /// </summary>
        public List<T_Status> Project { get; set; }
         
        /// <summary>
        /// Model containing unique status 
        /// </summary>
        public List<T_Status> Statuses { get; set; }

        /// <summary>
        /// Model containing all projects
        /// </summary>
        public List<T_Projects> ProjectList { get; set; }

        /// <summary>
        /// Model containing streams of department
        /// </summary>
        public List<T_Streams> Streams { get; set; }

        /// <summary>
        /// Model containing Releases
        /// </summary>
        public List<T_Releases> Release { get; set; }

        /// <summary>
        /// Model containing SubReleases
        /// </summary>
        public List<T_SubReleases> SubReleases { get; set; }

        ///// <summary>
        ///// Model containing Registration details
        ///// </summary>
        //public List<T_Registrations> Users { get; set; }     
    }
}