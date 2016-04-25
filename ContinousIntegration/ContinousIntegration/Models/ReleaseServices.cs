using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ContinousIntegration.Models
{
    public class ReleaseServices
    {
        private readonly ContinuousIntegrationEntities entities = new ContinuousIntegrationEntities();
        public IEnumerable<T_Releases> GetReleases()
        {
            return entities.T_Releases.ToList();
        }

        public List<NewReleaseModel> GetReleasePage(int pageNumber, int pageSize, string searchCriteria)
        {
            if (pageNumber < 1)
                pageNumber = 1;


            var RequiredReleaseModel = (from a in entities.T_Streams
                                        join b in entities.T_Releases
                                        on a.C_StreamID equals b.C_StreamID
                                        join c in entities.T_Status 
                                        on b.C_StatusID equals c.C_StatusID
                                        join d in entities.T_Projects
                                        on a.C_ProjectID equals d.C_ProjectID
                                        select new { a.C_StreamID, a.C_StreamName, b.C_ReleaseID, b.C_ReleaseName, b.C_StatusID,
                                            b.C_LastModified,c.C_StatusName,d.C_ProjectID,d.C_ProjectName })
                                     .ToList();

            //Populate NewReleaseModel with the required properties
          
            List<NewReleaseModel> release = new List<NewReleaseModel>();

            NewReleaseModel newobj;
            foreach(var item in RequiredReleaseModel)
            {
                newobj = new NewReleaseModel();

                newobj.C_ReleaseID = item.C_ReleaseID;
                newobj.C_ReleaseName = item.C_ReleaseName;
                newobj.C_StreamID = item.C_StreamID;
                newobj.C_StreamName = item.C_StreamName;
                newobj.C_StatusName= item.C_StatusName;
                newobj.C_LastModified = item.C_LastModified;
                newobj.C_ProjectName = item.C_ProjectName;
                newobj.C_ProjectID = item.C_ProjectID;

                release.Add(newobj);

            }

            return release;
        }

        public int CountAllReleases()
        {
            return entities.T_Releases.Count();
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        //To add new Project
        public bool AddRelease(NewReleaseModel trelease)
        {
            try
            {
               //Populate T_Releases with NewReleaseModel
               
                T_Releases relobj = new T_Releases()
                {
                    C_StreamID = trelease.C_StreamID,
                    C_ReleaseID = trelease.C_ReleaseID,
                    C_ReleaseName = trelease.C_ReleaseName,
                    C_StatusID = trelease.C_StatusID,
                    C_LastModified = DateTime.Now

                };

                entities.T_Releases.Add(relobj);
                entities.SaveChanges();
                //ModelState.Clear();
                //trelease = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        //To update Release details
        public bool UpdateRelease(NewReleaseModel trelease)
        {
            try
            {

                //Populate T_Releases with NewRelease Model
                T_Releases relobj = new T_Releases()
                {
                    C_ReleaseID = trelease.C_ReleaseID,
                    C_ReleaseName = trelease.C_ReleaseName,
                    C_StatusID = trelease.C_StatusID,
                    C_StreamID = trelease.C_StreamID,
                    C_LastModified = DateTime.Now
                };

                entities.T_Releases.Add(relobj);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

          //To delete Release
        public bool DeleteReleases(int releaseId)
        {
            try
            {
                T_Releases data = entities.T_Releases.Where(m => m.C_ReleaseID == releaseId).FirstOrDefault();
                entities.T_Releases.Remove(data);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

       
    public class ReleaseModel
    {
        
        [ScaffoldColumn(false)]
        [Display(Name = "Release ID")]
        public int C_ReleaseID { get; set; }

        [Display(Name = "Release Name")]
        [Required(ErrorMessage = "Please Enter Release Name")]
        public string C_ReleaseName { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "StreamID")]
        public int C_StreamID { get; set; }

        [Display(Name = "Last Modified")]
        [ScaffoldColumn(false)]
        public DateTime C_LastModified { get; set; }

        public IEnumerable<T_Releases> Releases { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
    }
    }
}