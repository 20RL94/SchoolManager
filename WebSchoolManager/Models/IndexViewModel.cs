using DataSchoolManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSchoolManager.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Pupil> Pupils { get; set; }
        public IEnumerable<Form> Forms { get; set; }
        public int? SelectedForm { get; set; }
    }

    public class TestViewModel
    {
        public IEnumerable<Pupil> Pupils { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public int SelectedForm { get; set; }
    }

    public class MarkSelectionModel
    {
        public Pupil Pupil { get; set; }
        public int TestId { get; set; }
        public int SelectedForm { get; set; }
    }

    public class PlanModel
    {
        public IEnumerable<Subject> subjects { get; set; }
        public IEnumerable<Lesson> lessons { get; set; }
        public int SelectedForm { get; set; }
    }
}
