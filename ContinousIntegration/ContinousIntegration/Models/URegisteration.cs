using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class URegisteration
    {
       [Required(ErrorMessage="Please provide First name")]
        public string C_FirstName { get; set; }

        [Required(ErrorMessage = "Please provide Last name")]
        public string C_LastName { get; set; }

        [Required(ErrorMessage = "Please provide Email Address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",ErrorMessage="Email Address not valid")]
        public string C_EmailID { get; set; }

        [Required(ErrorMessage = "Please provide Password")]
        public string C_Password { get; set; }

        
        [Compare("C_Password", ErrorMessage = "does not match")]
        public string C_CnfPassword { get; set; }
    }
}