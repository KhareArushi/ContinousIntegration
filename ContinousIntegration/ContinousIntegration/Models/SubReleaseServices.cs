using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class SubReleaseServices
    {
        private readonly ContinuousIntegrationEntities entities = new ContinuousIntegrationEntities();
        public List<T_SubReleases> GetSubReleases()
        {
            return entities.T_SubReleases.ToList();
        }

        public List<NewSubReleaseModel> GetSubReleasePage(int pageNumber, int pageSize, string searchCriteria)
        {
            if (pageNumber < 1)
                pageNumber = 1;


            var RequiredSubReleaseModel = (from a in entities.T_Releases
                                           join b in entities.T_SubReleases
                                           on a.C_ReleaseID equals b.C_ReleaseID
                                           join c in entities.T_Status
                                           on b.C_StatusID equals c.C_StatusID
                                           join d in entities.T_Streams
                                           on a.C_StreamID equals d.C_StreamID
                                           join e in entities.T_Projects
                                           on d.C_ProjectID equals e.C_ProjectID
                                           select new
                                           {
                                               a.C_ReleaseID,
                                               a.C_ReleaseName,
                                               b.C_SubReleaseID,
                                               b.C_SubReleaseName,
                                               b.C_StatusID,
                                               b.C_LastModified,
                                               c.C_StatusName,
                                               e.C_ProjectID,
                                               e.C_ProjectName,
                                               d.C_StreamID,d.C_StreamName
                                           })
                                     .ToList();

            //Populate NewSubReleaseModel with the required properties

            List<NewSubReleaseModel> Subrelease = new List<NewSubReleaseModel>();

            NewSubReleaseModel newobj;
            foreach (var item in RequiredSubReleaseModel)
            {

                newobj = new NewSubReleaseModel();
                newobj.C_ReleaseID = item.C_ReleaseID;
                newobj.C_ReleaseName = item.C_ReleaseName;
                newobj.C_SubReleaseName = item.C_SubReleaseName;
                newobj.C_SubReleaseID = item.C_SubReleaseID;
                newobj.C_StatusID = item.C_StatusID;
                newobj.C_StatusName = item.C_StatusName;
                newobj.C_LastModified = item.C_LastModified;
                newobj.C_ProjectID = item.C_ProjectID;
                newobj.C_ProjectName = item.C_ProjectName;
                newobj.C_StreamID = item.C_StreamID;
                newobj.C_StreamName = item.C_StreamName;

                Subrelease.Add(newobj);
            }
            return Subrelease;
        }

        public int CountAllSubReleases()
        {
            return entities.T_SubReleases.Count();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        public T_SubReleases GetSubReleaseDetails(int SubreleaseId)
        {
            return entities.T_SubReleases.Where(m => m.C_SubReleaseID == SubreleaseId).FirstOrDefault();
        }

        //To add new Project
        public bool AddSubRelease(T_SubReleases tsubrelease)
        {
            try
            {
                //Populate T_SubReleases with NewSubReleaseModel

                T_SubReleases srelobj = new T_SubReleases()
                {
                    C_SubReleaseID = tsubrelease.C_SubReleaseID,
                    C_ReleaseID = tsubrelease.C_ReleaseID,
                    C_SubReleaseName = tsubrelease.C_SubReleaseName,
                    C_StatusID = tsubrelease.C_StatusID,
                    C_LastModified = DateTime.Now

                };
                entities.T_SubReleases.Add(srelobj);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //To update Sub Release details
        public bool UpdateSubRelease(NewSubReleaseModel tsubrelease)
        {
            try
            {
                //Populate T_SubReleases with NewSubRelease Model
                T_SubReleases relobj = new T_SubReleases()
                {
                    C_ReleaseID = tsubrelease.C_ReleaseID,
                    C_SubReleaseID = tsubrelease.C_SubReleaseID,
                    C_SubReleaseName = tsubrelease.C_SubReleaseName,
                    C_StatusID = tsubrelease.C_StatusID,
                    C_LastModified = DateTime.Now
                };
                entities.T_SubReleases.Add(relobj);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        //To delete Release
        public bool DeleteSubReleases(int SubreleaseId)
        {
            try
            {
                T_SubReleases data = entities.T_SubReleases.Where(m => m.C_SubReleaseID == SubreleaseId).FirstOrDefault();
                entities.T_SubReleases.Remove(data);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public class SubReleaseModel
        {

            [ScaffoldColumn(false)]
            [Display(Name = "SubRelease ID")]
            public int C_SubReleaseID { get; set; }

            [Display(Name = "Release Name")]
            [Required(ErrorMessage = "Please Enter Release Name")]
            public string C_SubReleaseName { get; set; }

            [ScaffoldColumn(false)]
            [Display(Name = "Release ID")]
            public int C_ReleaseID { get; set; }

            [ScaffoldColumn(false)]
            [Display(Name = "Status ID")]
            public int C_StatusID { get; set; }

            [ScaffoldColumn(false)]
            [Display(Name = "Stream ID")]
            public int C_StreamID { get; set; }

            [Display(Name = "Status")]
            [Required(ErrorMessage = "Please Enter Status Name")]
            public string C_StatusName { get; set; }

            [Display(Name = "Last Modified")]
            [ScaffoldColumn(false)]
            public DateTime C_LastModified { get; set; }

            public IEnumerable<T_SubReleases> SubReleases { get; set; }
            public int TotalRows { get; set; }
            public int PageSize { get; set; }
        }

    }
}