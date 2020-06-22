using Choise.Attrs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Choise.Models
{
    public class Teacher
    {
        public int Id { set; get; }
        [Required]
        [Forbidden("xxx")]
        public string Name { set; get; }

        public List<Discipline> Disciplines { set; get; }
    }
}
