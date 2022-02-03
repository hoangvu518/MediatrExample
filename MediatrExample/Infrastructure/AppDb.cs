using MediatrExample.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MediatrExample.Infrastructure
{
    public class AppDb: DbContext
    {
        public AppDb()
        {
        }
        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {
        }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
