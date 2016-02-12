using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class Releases
    {
        public int C_ReleaseID { get; set; }
        public int C_StreamID { get; set; }
        public string C_ReleaseName { get; set; }
        public int C_StatusID { get; set; }
        public System.DateTime C_LastModified { get; set; }
    }
}