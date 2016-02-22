using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class Streams
    {
        public int C_ProjectID { get; set; }
        public int C_StreamID { get; set; }
        public string C_StreamName { get; set; }
        public System.DateTime C_LastModified { get; set; }
    }
}