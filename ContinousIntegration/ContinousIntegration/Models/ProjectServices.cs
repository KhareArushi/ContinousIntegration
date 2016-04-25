using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ContinousIntegration.Models
{
    public class ProjectServices
    {

        private readonly ContinuousIntegrationEntities entities = new ContinuousIntegrationEntities();
        public IEnumerable<T_Projects> GetProjects()
        {
            return entities.T_Projects.ToList();
        }
        public IEnumerable<T_Projects> GetProjectPage(int pageNumber, int pageSize, string searchCriteria)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            return entities.T_Projects
                .OrderBy(m => m.C_ProjectID)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToList();
        }

        public int CountAllProjects()
        {
            return entities.T_Projects.Count();
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        public T_Projects GetProjectDetails(int id)
        {
            return entities.T_Projects.Where(m => m.C_ProjectID == id).FirstOrDefault();
        }

        //To add new Project
        public bool AddProject(T_Projects tproject)
        {
            try
            {
                entities.T_Projects.Add(tproject);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //To update project details
        public bool UpdateProject(T_Projects tproject)
        {
            try
            {
                T_Projects data = entities.T_Projects.Where(m => m.C_ProjectID == tproject.C_ProjectID).FirstOrDefault();

                data.C_ProjectID = tproject.C_ProjectID;
                data.C_ProjectName = tproject.C_ProjectName;
                data.C_ProjectDescription = tproject.C_ProjectDescription;
                data.C_LastModified = tproject.C_LastModified;
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //To delete project
          public bool DeleteProject(int id)
        {
            try
            {
                T_Projects data = entities.T_Projects.Where(m => m.C_ProjectID == id).FirstOrDefault();
                entities.T_Projects.Remove(data);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
 
    public class ProjectModel
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Project ID")]
        public int C_ProjectID { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "Please Enter Project Name")]
        public string C_ProjectName { get; set; }

        [Display(Name = "Project Description")]
        [Required(ErrorMessage = "Please Enter Project Description")]
        [UIHint("Multiline")]
        public string C_ProjectDescription { get; set; }

        [Display(Name = "Last Modified")]
        [ScaffoldColumn(false)]
        public System.DateTime C_LastModified { get; set; }

        public IEnumerable<T_Projects> Project { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
    }
}


   