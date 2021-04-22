using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Students;
using Test.Core.StudyHouses;

namespace Test.EntityFramework
{
    public class TestDbContext: DbContext
    {
        public TestDbContext(DbContextOptions options) 
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyHouse> StudyHouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<StudyHouse>().ToTable("StudyHouses");
        }
    }
}
