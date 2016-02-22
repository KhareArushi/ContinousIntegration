using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class Parent
    {
        public List<T_Status> Project { get; set; }
        public List<T_Projects> ProjectList { get; set; }
        public List<T_Streams> Streams { get; set; }
        public List<T_Releases> Release { get; set; }
        public List<T_SubReleases> SubReleases { get; set; }
        public List<T_Users> Users { get; set; }
     
    }
}