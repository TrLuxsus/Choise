using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    public class Student
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Group { set; get; }

        public List<StudDisc> StudDiscs { set; get; }
    }
}
