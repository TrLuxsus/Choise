using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Choise.Data;
using Microsoft.AspNetCore.Mvc;

namespace Choise.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ChoiseDbContext _db;

        public StudentsController(ChoiseDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Students);
        }
    }
}
    