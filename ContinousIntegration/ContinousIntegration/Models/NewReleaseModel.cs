using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class NewReleaseModel:T_Releases
    {
        public string C_StreamName { get; set; }
        public string C_StatusName { get; set; }
        public string C_ProjectName { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "Project ID")]
        public int C_ProjectID { get; set; }
    }
}