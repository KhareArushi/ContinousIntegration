using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class NewSubReleaseModel:T_SubReleases
    {
        public string C_ReleaseName {get;set;}
        public string C_StatusName { get; set; }
        public int C_ProjectID { get; set; }
        public string C_ProjectName { get; set; }
        public string C_StreamName { get; set; }
        public int C_StreamID { get; set; }
       
    }
}