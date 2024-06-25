using EnglishCourses.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace EnglishCourses.BusinessLogic.DBContext
{
    public class CourseContext : DbContext
    {
        public CourseContext() : base("name=English")
        {
        }

        public virtual DbSet<CourseDbTable> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseDbTable>()
                .HasMany(c => c.Chapters)
                .WithOptional()
                .HasForeignKey(c => c.CourseId);
        }

    }
}