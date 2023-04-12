using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class BackendAPIDbContext : DbContext
    {
        public BackendAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(e => e.Teacher)
                .WithMany(e => e.Courses)
                .HasForeignKey(e => e.Teacher_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(e => e.Student)
                .WithMany(e => e.CourseRegistration)
                .HasForeignKey(e => e.Student_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(e => e.Course)
                .WithMany(e => e.CourseRegistration)
                .HasForeignKey(e => e.Course_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
    }
}
