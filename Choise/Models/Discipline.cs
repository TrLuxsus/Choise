using Choise.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Choise.Models
{
    public class Discipline
    {
        public int Id { set; get; }
        [Required]
        [Forbidden("xxx")]
        public string Title { set; get; }
        public string Annotation { set; get; }

        public int TeacherId { set; get; }
        public Teacher Teacher { set; get; }
        public List<StudDisc> StudDiscs { set; get; }
    }
}
