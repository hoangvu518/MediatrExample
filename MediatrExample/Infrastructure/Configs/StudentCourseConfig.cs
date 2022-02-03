using MediatrExample.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatrExample.Infrastructure.Configs
{
    public class StudentCourseConfig: IEntityTypeConfiguration<StudentCourse>
    {
     
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasOne(d => d.Course)
                 .WithMany(p => p.StudentCourse)
                 .HasForeignKey(d => d.CourseId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_StudentCourse_Course");

            builder.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Student");
        }
    }
}
