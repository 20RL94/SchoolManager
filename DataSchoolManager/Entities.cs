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
        //[MaxLength(30)] // Einschränkung - Datenbank neu erstellen
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MatrikelNo { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }

        public virtual Form Form { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
    }

    public class Form
    {
        public int FormId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
    }
}
