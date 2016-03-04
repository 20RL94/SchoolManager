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
        public string Lastname { get; set;}
        public string Firstname { get; set; }
        public string MatrikelNo { get; set; }
        public string Sex { get; set; }
        public String Birthday { get; set; }
        public int FormId { get; set; }

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
        public static DtoPupil ToDto(this Pupil pupil)
        {
            return new DtoPupil
            {
                PupilId = pupil.PupilId,
                Lastname = pupil.Lastname,
                Firstname=pupil.Firstname,
                MatrikelNo=pupil.MatrikelNo,
                Birthday=pupil.Birthday.ToShortDateString(),
                FormId=pupil.FormId,
                Sex=pupil.Sex
            };
        }
    }
}
   