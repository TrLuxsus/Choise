using ChoiceA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Data
{
    public class DomainDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudDisc> StudsDiscs { get; set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudDisc>()
                 .HasKey(e => new { e.StudentId, e.DisciplineId });
        }
    }
}
