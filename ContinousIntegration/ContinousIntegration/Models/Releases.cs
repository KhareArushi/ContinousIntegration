using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class Releases
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Release ID")]
        public int C_ReleaseID { get; set; }

        [Display(Name = "Release Name")]
        [Required(ErrorMessage = "Please Enter Release Name")]
        public string C_ReleaseName { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "StreamID")]
        public string C_StreamID { get; set; }

        [Display(Name = "Last Modified")]
        [ScaffoldColumn(false)]
        public DateTime C_LastModified { get; set; }
        
    }
}