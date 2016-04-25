using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContinousIntegration.Models
{
    /// <summary>
    /// Model used while login
    /// </summary>
    public class UserLoginModel
    {       
        public string C_UserName { get; set; }
        public string C_UserPassword { get; set; }
    }
}