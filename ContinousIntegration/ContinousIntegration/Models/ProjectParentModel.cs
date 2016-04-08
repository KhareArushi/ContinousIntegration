using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    /// <summary>
    /// Contains all the models
    /// </summary>
    public class ProjectParentModel
    {
        /// <summary>
        /// Model containing the status details
        /// </summary>
        public List<T_Status> ProjectStatus { get; set; }
         
        /// <summary>
        /// Model containing unique status details
        /// </summary>
        public List<T_Status> Statuses { get; set; }

        /// <summary>
        /// Model containing all projects details
        /// </summary>
        public List<T_Projects> Projects { get; set; }

        /// <summary>
        /// Model containing streams of department
        /// </summary>
        public List<T_Streams> Streams { get; set; }

        /// <summary>
        /// Model containing Releases
        /// </summary>
        public List<T_Releases> Releases { get; set; }

        /// <summary>
        /// Model containing SubReleases
        /// </summary>
        public List<T_SubReleases> SubReleases { get; set; }
    }
}