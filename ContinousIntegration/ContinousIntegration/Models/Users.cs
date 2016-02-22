using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class Users
    {
        public int C_UserID { get; set; }
        [Required(ErrorMessage = "You forgot to enter a Username.")]
        public string C_UserName { get; set; }
        [Required]
        public string C_UserPassword { get; set; }
        public System.DateTime C_LastModified { get; set; }
    }
}