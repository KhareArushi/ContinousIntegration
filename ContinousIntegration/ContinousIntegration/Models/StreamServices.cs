using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class StreamServices
    {
        private readonly ContinuousIntegrationEntities entities = new ContinuousIntegrationEntities();
        public IEnumerable<T_Streams> GetStreams()
        {
            return entities.T_Streams.ToList();
        }

        public List<NewStreamModel> GetStreamPage(int pageNumber, int pageSize, string searchCriteria)
        {
            if (pageNumber < 1)
                pageNumber = 1;


            var RequiredProjectModel = (from a in entities.T_Projects
                                        join b in entities.T_Streams
                                        on a.C_ProjectID equals b.C_ProjectID
                                        select new { a.C_ProjectName, a.C_ProjectID, b.C_StreamID, b.C_StreamName, b.C_LastModified })
                                        .ToList();

            //Populate NewStreamModel with the required properties
          
            List<NewStreamModel> streams = new List<NewStreamModel>();

            NewStreamModel newobj; 
            foreach (var item in RequiredProjectModel)
            {
                newobj = new NewStreamModel();

                newobj.C_ProjectID = item.C_ProjectID;
                newobj.C_ProjectName = item.C_ProjectName;
                newobj.C_StreamID = item.C_StreamID;
                newobj.C_StreamName = item.C_StreamName;
                newobj.C_LastModified = item.C_LastModified;

                streams.Add(newobj);
            }

            return streams;

           // return IEnumerable<>
            //  .OrderBy(m => m.C_StreamID)
            //.Skip((pageNumber - 1) * pageSize)
            //.Take(pageSize)
            //.ToList();
        }

        public int CountAllStreams()
        {
            return entities.T_Streams.Count();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        public T_Streams GetStreamDetails(int streamId)
        {
            return entities.T_Streams.Where(m => m.C_StreamID == streamId).FirstOrDefault();
        }

        //To add new Stream
        public bool AddStream(T_Streams tstream)
        {
            try
            {
                entities.T_Streams.Add(tstream);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //To update Release details
        public bool UpdateStream(T_Streams tstream)
        {
            try
            {
                T_Streams data = entities.T_Streams.Where(m => m.C_StreamID == tstream.C_StreamID).FirstOrDefault();

                data.C_StreamID = tstream.C_StreamID;
                data.C_StreamName = tstream.C_StreamName;
                data.C_ProjectID = tstream.C_ProjectID;
                data.C_LastModified = tstream.C_LastModified;
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //To delete Release
        public bool DeleteStreams(int streamId)
        {
            try
            {
                T_Streams data = entities.T_Streams.Where(m => m.C_StreamID == streamId).FirstOrDefault();
                entities.T_Streams.Remove(data);
                entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public class StreamModel
        {

            [ScaffoldColumn(false)]
            [Display(Name = "Stream ID")]
            public int C_StreamID { get; set; }

            [Display(Name = "Stream Name")]
            [Required(ErrorMessage = "Please Enter Stream Name")]
            public string C_StreamName { get; set; }

            [ScaffoldColumn(false)]
            [Display(Name = "ProjectID")]
            public int C_ProjectID { get; set; }

            [Display(Name = "Project Name")]
            public string C_ProjectName { get; set; }

            [Display(Name = "Last Modified")]
            [ScaffoldColumn(false)]
            public DateTime C_LastModified { get; set; }

            public IEnumerable<NewStreamModel> Streams { get; set; }
            public int TotalRows { get; set; }
            public int PageSize { get; set; }
        }

    }
}