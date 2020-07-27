using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Choise.Models
{
    public class StudDisc
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}
