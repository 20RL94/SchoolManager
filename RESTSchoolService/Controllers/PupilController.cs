using DataSchoolManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTSchoolService.Controllers
{
    public class PupilController : ApiController
    {
        SchoolRepository<Pupil> rep = new SchoolRepository<Pupil>();
        // GET api/pupil
        public IEnumerable<PupilDto> Get()
        {
            return rep.Get()
                .Select(p=>new PupilDto { PupilId=p.PupilId,Firstname = p.Firstname, Lastname = p.Lastname});
        }

        // GET api/pupil/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/pupil
        public void Post([FromBody]Pupil value)
        {
            rep.Create(value);
        }

        // PUT api/pupil/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/pupil/5
        public void Delete(int id)
        {
        }
    }


    public class PupilDto
    {
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public int PupilId { get; set; }
    }
}
