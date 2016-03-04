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
        // ? ... Integer, der den Wert null haben kann (Nullable Value)
    }
}
