using ContosoUniversityBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityBlazor.Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollment");

        builder.HasIndex(e => e.CourseID);

        builder.HasIndex(e => e.StudentID);

        builder.Property(e => e.EnrollmentID).HasColumnName("EnrollmentID");

        builder.Property(e => e.CourseID).HasColumnName("CourseID");

        builder.Property(e => e.StudentID).HasColumnName("StudentID");

        builder.HasOne(d => d.Course)
            .WithMany(p => p.Enrollments)
            .HasForeignKey(d => d.CourseID);

        builder.HasOne(d => d.Student)
            .WithMany(p => p.Enrollments)
            .HasForeignKey(d => d.StudentID);
    }
}
