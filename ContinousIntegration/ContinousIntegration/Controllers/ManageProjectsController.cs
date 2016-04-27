using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ContinousIntegration.Models;

namespace ContinousIntegration.Controllers
{
    public class ManageProjectsController : Controller
    {
        ProjectServices mobjModel = new ProjectServices();
        ReleaseServices robjModel = new ReleaseServices();
        StreamServices sobjModel = new StreamServices();
        SubReleaseServices srobjModel = new SubReleaseServices();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///     Method to get the configuration view
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfigView()
        {
            return View();
        }
        /// <summary>
        /// Method to get the webgrid of the project
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        public ActionResult SearchProject(int page = 1, string sort = "name", string sortDir = "ASC")
        {
            const int pageSize = 5;
            var totalRows = mobjModel.CountAllProjects();

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Project ID", "Project Name", "Project Description", "Last Modified" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "id";

            var project = mobjModel.GetProjectPage(page, pageSize, "it." + sort + " " + sortDir);

            var data = new ProjectModel()
             {
                 TotalRows = totalRows,
                 PageSize = pageSize,
                 Project = project
             };
            return View(data);
        }
        /// <summary>
        /// Method to add new project
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Add Project
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.IsUpdate = false;
                return View("_CreateProject");

            }
            else
                return View();
        }


        [HttpPost]
        public ActionResult CreateProject(ProjectModel tproject, string Command)
        {

            T_Projects Obj = new T_Projects();
            Obj.C_ProjectName = tproject.C_ProjectName;
            Obj.C_ProjectDescription = tproject.C_ProjectDescription;
            Obj.C_LastModified = DateTime.Now;

            bool IsSuccess = mobjModel.AddProject(Obj);
            if (IsSuccess)
            {
                TempData["OperStatus"] = "Project added successfully";
                ModelState.Clear();
                return RedirectToAction("SearchProject", "ManageProjects");
            }


            return PartialView("_CreateProject");
        }

        /// <summary>
        /// Method to populate the edit popup
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Edit Project
        public ActionResult EditProject(int id)
        {
            var data = mobjModel.GetProjectDetails(Convert.ToInt32(id));


            ProjectModel Obj = new ProjectModel();
            Obj.C_ProjectID = data.C_ProjectID;
            Obj.C_ProjectName = data.C_ProjectName;
            Obj.C_ProjectDescription = data.C_ProjectDescription;
            Obj.C_LastModified = data.C_LastModified;


            ViewBag.IsUpdate = true;
            return View("_EditProject", Obj);

        }

        /// <summary>
        /// Method to update the changes 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Update details of project
        [HttpPost]
        public ActionResult UpdateProject(ProjectModel tproject, string Command)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditProject", tproject);
            }
            else
            {
                T_Projects Obj = new T_Projects();
                Obj.C_ProjectID = tproject.C_ProjectID;
                Obj.C_ProjectName = tproject.C_ProjectName;
                Obj.C_ProjectDescription = tproject.C_ProjectDescription;
                Obj.C_LastModified = DateTime.Now;

                bool IsSuccess = mobjModel.UpdateProject(Obj);
                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Project updated successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchProject", "ManageProjects");
                }
            }

            return PartialView("_EditProject");
        }

        /// <summary>
        /// Method to delete project
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //To delete Project
        public ActionResult Delete(int id)
        {
            bool check = mobjModel.DeleteProject(id);
            var data = mobjModel.GetProjects();
            return RedirectToAction("SearchProject");
        }



        //**********StreamGrid**************
        /// <summary>
        /// Method to get the webgrid of the stream
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        public ActionResult SearchStream(int page = 1, string sort = "name", string sortDir = "ASC")
        {
            const int pageSize = 5;
            var totalRows = sobjModel.CountAllStreams();

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Stream ID", "Stream Name", "Project ID", "Last Modified" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "id";


            var stream = sobjModel.GetStreamPage(page, pageSize, "it." + sort + " " + sortDir);
            var data = new ContinousIntegration.Models.StreamServices.StreamModel
            {
                TotalRows = totalRows,
                PageSize = pageSize,
                Streams = stream
            };
            return View(data);
        }

        /// <summary>
        /// Method to add new project
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Add Streams
        public ActionResult CreateS()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.IsUpdate = false;
                ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();
                ViewBag.ProjectName = new SelectList(ci.T_Projects, "C_ProjectID", "C_ProjectName");
                return View("_CreateStream");
            }
            else

                return View();
        }

        [HttpPost]
        public ActionResult CreateStream(ContinousIntegration.Models.NewStreamModel tstream, string Command)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateStream", tstream);
            }
            else
            {
                T_Streams Obj = new T_Streams();
                Obj.C_StreamName = tstream.C_StreamName;
                Obj.C_ProjectID = tstream.C_ProjectID;
                Obj.C_LastModified = DateTime.Now;

                bool IsSuccess = sobjModel.AddStream(Obj);


                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Stream added successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchStream", "ManageProjects");
                }
            }

            return PartialView("_CreateStream");
        }
        /// <summary>
        /// Method to populate the edit popup
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Edit Stream
        public ActionResult EditStream(int streamId)
        {
            ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();
            var data = sobjModel.GetStreamDetails(streamId);

            if (Request.IsAjaxRequest())
            {
                NewStreamModel Obj = new NewStreamModel();
                Obj.C_StreamName = data.C_StreamName;
                Obj.C_ProjectID = data.C_ProjectID;
                Obj.C_StreamID = data.C_StreamID;
                Obj.C_LastModified = data.C_LastModified;


                ViewBag.ProjectName = new SelectList(ci.T_Projects, "C_ProjectID", "C_ProjectName");
                ViewBag.IsUpdate = true;
                return View("_EditStream", Obj);
            }
            else
                return View(data);
        }

        /// <summary>
        /// Method to update the changes 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Update details of Streams
        [HttpPost]
        public ActionResult UpdateStream(ContinousIntegration.Models.NewStreamModel tstream, string Command)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditStream", tstream);
            }
            else
            {
                T_Streams Obj = new T_Streams();
                Obj.C_StreamName = tstream.C_StreamName;
                Obj.C_ProjectID = tstream.C_ProjectID;
                Obj.C_StreamID = tstream.C_StreamID;
                Obj.C_LastModified = DateTime.Now;


                bool IsSuccess = sobjModel.UpdateStream(Obj);
                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Stream updated successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchStream", "ManageProjects");
                }
            }

            return PartialView("_EditRelease");
        }

        /// <summary>
        /// Method to delete stream
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //To delete Stream
        public ActionResult DeleteS(int streamId)
        {
            bool check = sobjModel.DeleteStreams(streamId);
            var data = sobjModel.GetStreams();
            return RedirectToAction("SearchStream");
        }



        //*********ReleaseGrid***************

        /// <summary>
        /// Method to get the release webgrid 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        public ActionResult SearchRelease(int page = 1, string sort = "name", string sortDir = "ASC")
        {
            const int pageSize = 5;
            var totalRows = robjModel.CountAllReleases();

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "Release ID", "Release Name", "Stream ID", "Project Name", "Last Modified" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "id";

            var release = robjModel.GetReleasePage(page, pageSize, "it." + sort + " " + sortDir);
            var data = new ContinousIntegration.Models.ReleaseServices.ReleaseModel()
            {
                TotalRows = totalRows,
                PageSize = pageSize,
                Releases = release
            };
            return View(data);
        }

        /// <summary>
        /// Method to add new Release
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Add Release
        public ActionResult CreateR()
        {
            if (Request.IsAjaxRequest())
            {
                List<T_Projects> allproject = new List<T_Projects>();
                List<T_Streams> allstream = new List<T_Streams>();
                ViewBag.IsUpdate = false;
                using (ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities())
                {
                    allproject = ci.T_Projects.OrderBy(a => a.C_ProjectName).ToList();
                }
                ContinuousIntegrationEntities c = new ContinuousIntegrationEntities();
                ViewBag.StatusName = new SelectList(c.T_Status, "C_StatusID", "C_StatusName");
                ViewBag.ProjectName = new SelectList(allproject, "C_ProjectID", "C_ProjectName");
                ViewBag.StreamName = new SelectList(allstream, "C_StreamID", "C_StreamName");
                return View("_CreateRelease");
            }
            else

                return View();
        }



        [HttpPost]
        public ActionResult CreateRelease(ContinousIntegration.Models.NewReleaseModel trelease, string Command)
        {

            if (!ModelState.IsValid)
            {
                return PartialView("_CreateRelease", trelease);
            }
            else
            {
                T_Releases Obj = new T_Releases();
                Obj.C_ReleaseName = trelease.C_ReleaseName;
                Obj.C_StreamID = trelease.C_StreamID;
                Obj.C_StatusID = trelease.C_StatusID;
                Obj.C_LastModified = DateTime.Now;

                bool IsSuccess = robjModel.AddRelease(trelease);
                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Release added successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchRelease", "ManageProjects");
                }
            }

            return PartialView("_CreateRelease");
        }

        /// <summary>
        /// Method to get cascading dropdown list 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        public JsonResult GetStreamByProject(string projectID)
        {
            JsonResult jsonResult;
            int id;
            if (int.TryParse(projectID, out id))
            {
                using (var ci = new ContinuousIntegrationEntities())
                {
                    var mappedStreams = (from stream in ci.T_Streams
                                         where stream.C_ProjectID == id
                                         select new { Id = stream.C_ProjectID, Name = stream.C_StreamName }).ToList();

                    return Json(mappedStreams, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Not Valid Request", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to populate the edit popup
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Edit Release
        public ActionResult EditRelease(int releaseId)
        {
            ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();
            var data = (from a in ci.T_Projects
                        join b in ci.T_Streams
                          on a.C_ProjectID equals b.C_ProjectID
                        join c in ci.T_Releases
                          on b.C_StreamID equals c.C_StreamID
                        where releaseId == c.C_ReleaseID
                        select new { a.C_ProjectID, c.C_ReleaseID, c.C_ReleaseName, c.C_StatusID, c.C_StreamID, c.C_LastModified, b.C_StreamName }).FirstOrDefault();
            NewReleaseModel Obj;
            if (Request.IsAjaxRequest())
            {
                Obj = new NewReleaseModel();
                Obj.C_ReleaseName = data.C_ReleaseName;
                Obj.C_ReleaseID = data.C_ReleaseID;
                Obj.C_StreamID = data.C_StreamID;
                Obj.C_StatusID = data.C_StatusID;
                Obj.C_LastModified = data.C_LastModified;
                Obj.C_ProjectID = data.C_ProjectID;

                ViewBag.StreamName = new SelectList(ci.T_Streams.Where(x=>x.C_ProjectID.Equals(data.C_ProjectID)), "C_StreamID", "C_StreamName",ci.T_Streams.Where(x=>x.C_StreamID.Equals(data.C_StreamID)));
                ViewBag.StatusName = new SelectList(ci.T_Status, "C_StatusID", "C_StatusName");
                ViewBag.ProjectName = new SelectList(ci.T_Projects, "C_ProjectID", "C_ProjectName");
                ViewBag.IsUpdate = true;
                return View("_EditRelease", Obj);
            }
            else
                return View(data);
        }

        /// <summary>
        /// Method to update the changes 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Update details of Release
        [HttpPost]
        public ActionResult UpdateRelease(ContinousIntegration.Models.NewReleaseModel trelease, string Command)
        {
            bool IsSuccess = robjModel.UpdateRelease(trelease);
            if (IsSuccess)
            {
                TempData["OperStatus"] = "Release updated successfully";
                ModelState.Clear();
                return RedirectToAction("SearchRelease", "ManageProjects");
            }


            return PartialView("_EditRelease");
        }

        /// <summary>
        /// Method to delete stream
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //To delete Release
        public ActionResult DeleteR(int releaseId)
        {
            bool check = robjModel.DeleteReleases(releaseId);
            var data = robjModel.GetReleases();
            return RedirectToAction("SearchRelease");
        }


        //*********SubRelease Grid***************
        /// <summary>
        /// Method to get the Sub-release webgrid 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        public ActionResult SearchSubRelease(int page = 1, string sort = "name", string sortDir = "ASC")
        {
            const int pageSize = 5;
            var totalRows = srobjModel.CountAllSubReleases();

            sortDir = sortDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? sortDir : "asc";

            var validColumns = new[] { "SubRelease Name", "Release Name", "Status", "Project Name", "Last Modified" };

            if (!validColumns.Any(c => c.Equals(sort, StringComparison.CurrentCultureIgnoreCase)))
                sort = "id";

            var subrelease = srobjModel.GetSubReleasePage(page, pageSize, "it." + sort + " " + sortDir);
            var data = new ContinousIntegration.Models.SubReleaseServices.SubReleaseModel()
            {
                TotalRows = totalRows,
                PageSize = pageSize,
                SubReleases = subrelease
            };
            return View(data);
        }

        /// <summary>
        /// Method to add new Sub-Release
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Add SubRelease
        public ActionResult CreateSR()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.IsUpdate = false;
                ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();

                ViewBag.StatusName = new SelectList(ci.T_Status, "C_StatusID", "C_StatusName");
                ViewBag.ProjectName = new SelectList(ci.T_Projects, "C_ProjectID", "C_ProjectName");
                ViewBag.ReleaseName = new SelectList(ci.T_Releases, "C_ReleaseID", "C_ReleaseName");
                ViewBag.StreamName = new SelectList(ci.T_Streams, "C_StreamID", "C_StreamName");
                return View("_CreateSubRelease");
            }
            else

                return View();
        }

        [HttpPost]
        public ActionResult CreateSubRelease(ContinousIntegration.Models.NewSubReleaseModel tsubrelease, string Command)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateSubRelease", tsubrelease);
            }
            else
            {
                T_SubReleases Obj = new T_SubReleases();
                Obj.C_SubReleaseName = tsubrelease.C_SubReleaseName;
                Obj.C_SubReleaseID = tsubrelease.C_SubReleaseID;
                Obj.C_ReleaseID = tsubrelease.C_ReleaseID;
                Obj.C_StatusID = tsubrelease.C_StatusID;
                Obj.C_LastModified = DateTime.Now;

                bool IsSuccess = srobjModel.AddSubRelease(Obj);
                if (IsSuccess)
                {
                    TempData["OperStatus"] = "Sub-Release added successfully";
                    ModelState.Clear();
                    return RedirectToAction("SearchSubRelease", "ManageProjects");
                }
            }

            return PartialView("_CreateSubRelease");
        }


        /// <summary>
        /// Method to populate the edit popup
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Edit SubRelease
        public ActionResult EditSubRelease(int subreleaseId)
        {
            ContinuousIntegrationEntities ci = new ContinuousIntegrationEntities();
            var data = (from a in ci.T_Projects
                        join b in ci.T_Streams
                          on a.C_ProjectID equals b.C_ProjectID
                        join c in ci.T_Releases
                        on b.C_StreamID equals c.C_StreamID
                        join d in ci.T_SubReleases
                          on c.C_ReleaseID equals d.C_ReleaseID
                        where subreleaseId == d.C_SubReleaseID
                        select new
                        {
                            a.C_ProjectID,
                            c.C_ReleaseID,
                            d.C_SubReleaseName,
                            d.C_SubReleaseID,
                            d.C_StatusID,
                            d.C_LastModified,
                            b.C_StreamID,
                            b.C_StreamName
                        }).FirstOrDefault();

            NewSubReleaseModel Obj;
            if (Request.IsAjaxRequest())
            {
                Obj = new NewSubReleaseModel();
                Obj.C_SubReleaseName = data.C_SubReleaseName;
                Obj.C_ReleaseID = data.C_ReleaseID;
                Obj.C_SubReleaseID = data.C_SubReleaseID;
                Obj.C_StatusID = data.C_StatusID;
                Obj.C_LastModified = data.C_LastModified;
                Obj.C_ProjectID = data.C_ProjectID;
                Obj.C_StreamID = data.C_StreamID;
                Obj.C_StreamName = data.C_StreamName;

                ViewBag.ProjectName = new SelectList(ci.T_Projects, "C_ProjectID", "C_ProjectName");
                ViewBag.ReleaseName = new SelectList(ci.T_Releases, "C_ReleaseID", "C_ReleaseName");
                ViewBag.StatusName = new SelectList(ci.T_Status, "C_StatusID", "C_StatusName");
                ViewBag.StreamName = new SelectList(ci.T_Streams, "C_StreamID", "C_StreamName");
                ViewBag.IsUpdate = true;
                return View("_EditSubRelease", Obj);
            }
            else
                return View(data);
        }

        /// <summary>
        /// Method to update the changes 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //Update details of SubRelease
        [HttpPost]
        public ActionResult UpdateSubRelease(ContinousIntegration.Models.NewSubReleaseModel tsubrelease, string Command)
        {

            bool IsSuccess = srobjModel.UpdateSubRelease(tsubrelease);
            if (IsSuccess)
            {
                TempData["OperStatus"] = "Sub-Release updated successfully";
                ModelState.Clear();
                return RedirectToAction("SearchSubRelease", "ManageProjects");
            }


            return PartialView("_EditSubRelease");
        }

        /// <summary>
        /// Method to delete stream
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        //To delete SubRelease
        public ActionResult DeleteSR(int SubreleaseId)
        {
            bool check = srobjModel.DeleteSubReleases(SubreleaseId);
            var data = srobjModel.GetSubReleases();
            return RedirectToAction("SearchSubRelease");
        }

    }
}

    

