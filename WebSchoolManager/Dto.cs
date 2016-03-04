using DataSchoolManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSchoolManager
{
    public class DtoForm
    {
        public int FormId { get; set; }
        public string Name { get; set; }

    }

    public class DtoPupil
    {
        public int PupilId { get; set; }
        //...
    }

    public static class DtoExtensions
    {
        public static DtoForm ToDto(this Form form)
        {
            return new DtoForm
            {
                FormId = form.FormId,
                Name = form.Name
            };
        }
    }

}