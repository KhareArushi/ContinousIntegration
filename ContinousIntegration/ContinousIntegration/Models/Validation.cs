using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContinousIntegration.Models
{
    public class Validation
    {
        [Required(ErrorMessage = "Please provide User name")]
        public string C_UserName { get; set; }

        [Required(ErrorMessage = "Please provide Password")]
        public string C_UserPassword { get; set; }
    }
}