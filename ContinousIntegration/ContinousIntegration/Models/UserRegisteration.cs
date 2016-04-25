using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    /// <summary>
    /// Class used for user registration
    /// </summary>
    public class UserRegisteration
    {

        public object reg;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserRegisteration()
        {

        }

        [Required(ErrorMessage = "Please provide First name")]
        public string C_FirstName { get; set; }

        [Required(ErrorMessage = "Please provide Last name")]
        public string C_LastName { get; set; }

        [Required(ErrorMessage = "Please provide Email Address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email Address not valid")]
        public string C_EmailID { get; set; }

        [Required(ErrorMessage = "Please provide Password")]
        [StringLength(6, ErrorMessage = "Password must be a maximum of 6 characters.")]
        public string C_Password { get; set; }

        [Compare("C_Password", ErrorMessage = "Password does not match")]
        public string C_CnfPassword { get; set; }


    }
}