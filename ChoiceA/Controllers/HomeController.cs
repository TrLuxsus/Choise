﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChoiceA.Models;
using Microsoft.AspNetCore.Authorization;
using ChoiceA.Data;
using Microsoft.EntityFrameworkCore;

namespace ChoiceA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DomainDbContext _context;

        public HomeController(ILogger<HomeController> logger, DomainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return  View(_context.Students.ToList());
        }

        // GET: Home/Select/5
        public IActionResult Select(int? id)
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
        public IActionResult Select(int studentId, int[] selDiscIds)
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
