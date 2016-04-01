using System;

namespace DataSchoolManager
{
    public class DtoForm
    {
         public int FormId { get; set; }
         public string Name { get; set; }
    }

    public class DtoPupil
    {
        public int PupilId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MatrikelNo { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Form { get; set; }
    }

    public static class DtoExtensions
    {
        public static DtoForm ToDto(this Form form)
        {
            return new DtoForm {FormId = form.FormId, Name = form.Name};
        }

        public static DtoPupil ToDto(this Pupil p)
        {
            return new DtoPupil {PupilId = p.PupilId, Firstname = p.Firstname, Lastname = p.Lastname, Birthday = p.Birthday, Sex = p.Sex, MatrikelNo =  p.MatrikelNo, Form = p.Form.Name};
        }
    }
}