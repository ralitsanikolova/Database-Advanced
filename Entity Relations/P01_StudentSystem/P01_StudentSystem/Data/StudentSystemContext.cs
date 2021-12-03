using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    class StudentSystemContext : DbContext
    {
        public StudentSystemContext(){}
        public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
            : base(options) {}  

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Student> StudentsEnrolled { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer("Server=.;Database=EntityRelations;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Name)
                .IsUnicode();

                entity.Property(e => e.Description)
                .IsRequired(false)
                .IsUnicode();
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Content)
                .IsUnicode(false);

                entity.HasOne(e => e.Course)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(e => e.StudentId);

                entity.HasOne(e => e.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(s => s.CourseId);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(e => e.Name)
                .IsUnicode();

                entity.Property(e => e.URL)
                .IsUnicode(false);

                entity.HasOne(e => e.Course)
                .WithMany(r => r.Resources)
                .HasForeignKey(e => e.CourseId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Name)
                .IsUnicode();

                entity.Property(e => e.PhoneNumber)
                .IsUnicode(false)
                .IsRequired(false);

                entity.Property(e => e.Birthday)
                .IsRequired(false);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId });

                entity.HasOne(e => e.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(e => e.CourseId);

                entity.HasOne(e => e.Student)
                .WithMany(c => c.Courses)
                .HasForeignKey(e => e.StudentId);
            });
        }
    }
}
