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
            if (id == null)
            {
                return NotFound();
            }

            var selectedDisciplines =
                await _context.StudsDiscs.
                Where(i => i.StudentId == id).
                Select(i => i.Discipline).
                ToListAsync();

            ViewBag.Selected = selectedDisciplines;

            return View(await _context.Disciplines.ToListAsync());
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
