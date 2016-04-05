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
            using (var db = new ContinuousIntegrationEntities())
            {
                var q = from data in db.T_Status select data;
                return q.ToList();
            }
        }

        public List<T_Projects> GetallProjects()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var q = from data in db.T_Projects select data;
                return q.ToList();
            }
        }

        public List<T_Streams> GetallStreams()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var q = from data in db.T_Streams select data;
                return q.ToList();
            }
        }

        public List<T_Releases> GetallReleases()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var q = from data in db.T_Releases select data;
                return q.ToList();
            }
        }

        public List<T_SubReleases> GetallSubReleases()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var q = from data in db.T_SubReleases select data;
                return q.ToList();
            }
        }

        public List<T_Registrations> GetallUsers()
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                var q = from data in db.T_Registrations select data;
                return q.ToList();
            }
        }

        public void SaveDetails(URegisteration register)
        {
            using (var db = new ContinuousIntegrationEntities())
            {
                T_Registrations xyz = new T_Registrations();
                xyz.C_FirstName = register.C_FirstName;
                xyz.C_LastName = register.C_LastName;
                xyz.C_EmailID = register.C_EmailID;
                xyz.C_Password = register.C_Password;
               // xyz.C_CnfPassword = register.C_CnfPassword;
                db.T_Registrations.Add(xyz);
                db.SaveChanges();

            }


        }


    }
}