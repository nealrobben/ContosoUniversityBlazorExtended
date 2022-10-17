using ContosoUniversityBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityBlazor.Persistence.Configurations;

public class CourseAssignmentConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> builder)
    {
        builder.ToTable("CourseAssignment");

        builder.HasKey(e => new { e.CourseID, e.InstructorID });

        builder.HasIndex(e => e.InstructorID);

        builder.Property(e => e.CourseID).HasColumnName("CourseID");

        builder.Property(e => e.InstructorID).HasColumnName("InstructorID");

        builder.HasOne(d => d.Course)
            .WithMany(p => p.CourseAssignments)
            .HasForeignKey(d => d.CourseID);

        builder.HasOne(d => d.Instructor)
            .WithMany(p => p.CourseAssignments)
            .HasForeignKey(d => d.InstructorID);
    }
}
