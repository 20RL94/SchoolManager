using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSchoolManager
{
    public class Pupil
    {
        public int PupilId { get; set; }
        [MaxLength(30, ErrorMessage="max 30 Zeichen")]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [RegularExpression("[0-9]*")]
        public string MatrikelNo { get; set; }
        [MaxLength(1)]
        [MinLength(1)]
        public string Sex { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Datumsformat ungültig")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy)}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public virtual Form Form { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }

    public class Form
    {
        public int FormId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
    }

    public class Test
    {
        public int TestId { get; set; }
        public string Description { get; set; }
        public virtual Form Form { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
    }

    public class Mark
    {
        public int MarkId { get; set; }
        public int Value { get; set; }
        public virtual Pupil Pupil { get; set; }
        [ForeignKey("Pupil")]
        public int PupilId { get; set; }
        public virtual Test Test { get; set; }
        [ForeignKey("Test")]
        public int TestId { get; set; }
    }

    public class Subject
    {
        public int SubjectId { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }

    public class Lesson
    {
        public int LessonId { get; set; }
        public virtual Subject Subject { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public int Unit { get; set; }
        public int DayOfWeek { get; set; }
        public virtual Form Form { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
    }
}
