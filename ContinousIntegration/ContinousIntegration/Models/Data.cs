using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class Data
    {
        public List<T_Status> GetallStatus()
        {
            using (var db = new CIEntities())
            {
                var q = from data in db.T_Status select data;
                return q.ToList();
            }
        }

        public List<T_Projects> GetallProjects()
        {
            using (var db = new CIEntities())
            {
                var q = from data in db.T_Projects select data;
                return q.ToList();
            }
        }

        public List<T_Streams> GetallStreams()
        {
            using (var db = new CIEntities())
            {
                var q = from data in db.T_Streams select data;
                return q.ToList();
            }
        }

        public List<T_Releases> GetallReleases()
        {
            using (var db = new CIEntities())
            {
                var q = from data in db.T_Releases select data;
                return q.ToList();
            }
        }

        public List<T_SubReleases> GetallSubReleases()
        {
            using (var db = new CIEntities())
            {
                var q = from data in db.T_SubReleases select data;
                return q.ToList();
            }
        }

        public List<T_Users> GetallUsers()
        {
            using (var db = new CIEntities())
            {
                var q = from data in db.T_Users select data;
                return q.ToList();
            }
        }


    }
}