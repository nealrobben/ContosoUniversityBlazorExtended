using ContosoUniversityBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityBlazor.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasIndex(e => e.DepartmentID);

        builder.Property(e => e.CourseID)
            .HasColumnName("CourseID")
            .ValueGeneratedNever();

        builder.Property(e => e.DepartmentID)
            .HasColumnName("DepartmentID")
            .HasDefaultValueSql("((1))");

        builder.Property(e => e.Title).HasMaxLength(50);

        builder.HasOne(d => d.Department)
            .WithMany(p => p.Courses)
            .HasForeignKey(d => d.DepartmentID);
    }
}
