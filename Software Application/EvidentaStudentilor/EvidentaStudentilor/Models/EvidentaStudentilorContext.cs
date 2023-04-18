using Microsoft.EntityFrameworkCore;

namespace EvidentaStudentilor.Models
{
    public class EvidentaStudentilorContext : DbContext
    {
        public EvidentaStudentilorContext(DbContextOptions<EvidentaStudentilorContext> options)
            : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Curricula> Curricula { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
