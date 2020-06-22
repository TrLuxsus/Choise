using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Choise.Models;
using Choise.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;

namespace Choise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChoiseDbContext _context;

        public HomeController(ILogger<HomeController> logger, ChoiseDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Home/Select/5
        public async Task<IActionResult> Select(int? id)
        {
            var student = _context.Students.Include("StudDiscs").SingleOrDefault(s => s.Id == id);
            var selDiscIds = student.StudDiscs.Select(d => d.DisciplineId);
            var discs = _context.Disciplines;

            var model = new HomeSelectViewModel { Student = student };
            model.SelDiscs = discs.Where(d => selDiscIds.Contains(d.Id)).OrderBy(d => d.Title);
            model.NonSelDiscs = discs.Except(model.SelDiscs).OrderBy(d => d.Title);

            return View(model);
        }

        // POST: Home/Select/5
        [HttpPost]
        public async Task<IActionResult> Select(int studentId, int[] selDiscIds)
        {
            var student = _context.Students.Include("StudDiscs").SingleOrDefault(s => s.Id == studentId);
            student.StudDiscs = new List<StudDisc>();

            foreach (var id in selDiscIds)
                student.StudDiscs.Add(new StudDisc { StudentId = student.Id, DisciplineId = id });

            _context.SaveChanges();

            return RedirectToAction("Select");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
