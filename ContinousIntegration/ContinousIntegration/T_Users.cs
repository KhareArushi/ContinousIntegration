//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContinousIntegration
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Users
    {
        public T_Users()
        {
            this.T_UserProjectMappings = new HashSet<T_UserProjectMappings>();
            this.T_UserRoleMappings = new HashSet<T_UserRoleMappings>();
        }
    
        public int C_UserID { get; set; }
        public string C_UserName { get; set; }
        public string C_UserPassword { get; set; }
        public System.DateTime C_LastModified { get; set; }
    
        public virtual ICollection<T_UserProjectMappings> T_UserProjectMappings { get; set; }
        public virtual ICollection<T_UserRoleMappings> T_UserRoleMappings { get; set; }
    }
}
