using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class URegisteration
    {
        [Required(ErrorMessage = "You forgot to enter a User Name.")]
        public string UFirstName { get; set; }
        public string ULastName { get; set; }

        [Required]
        public string UPassword { get; set; }

        [Required(ErrorMessage = "Email is required (we promise not to spam you!).")]
        [DataType(DataType.EmailAddress)]
        public string UEmailID { get; set; }

        public string URoles { get; set; }
        public string UProjects { get; set; }
    }
}