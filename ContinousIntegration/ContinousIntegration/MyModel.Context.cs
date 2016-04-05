﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class ContinuousIntegrationEntities : DbContext
    {
        public ContinuousIntegrationEntities()
            : base("name=ContinuousIntegrationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<T_Projects> T_Projects { get; set; }
        public DbSet<T_Registrations> T_Registrations { get; set; }
        public DbSet<T_ReleaseMappings> T_ReleaseMappings { get; set; }
        public DbSet<T_Releases> T_Releases { get; set; }
        public DbSet<T_Roles> T_Roles { get; set; }
        public DbSet<T_Status> T_Status { get; set; }
        public DbSet<T_Streams> T_Streams { get; set; }
        public DbSet<T_SubReleases> T_SubReleases { get; set; }
        public DbSet<T_UserProjectMappings> T_UserProjectMappings { get; set; }
        public DbSet<T_UserRoleMappings> T_UserRoleMappings { get; set; }
    
        public virtual ObjectResult<GetAllProjects_Result> GetAllProjects(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllProjects_Result>("GetAllProjects", userIdParameter);
        }
    
        public virtual ObjectResult<ProjectDetails_Result> ProjectDetails(Nullable<int> userID, Nullable<int> projectID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProjectDetails_Result>("ProjectDetails", userIDParameter, projectIDParameter);
        }
    }
}
