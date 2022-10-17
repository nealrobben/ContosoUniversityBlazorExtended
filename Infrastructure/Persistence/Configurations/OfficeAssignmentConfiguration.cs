using ContosoUniversityBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityBlazor.Persistence.Configurations;

public class OfficeAssignmentConfiguration : IEntityTypeConfiguration<OfficeAssignment>
{
    public void Configure(EntityTypeBuilder<OfficeAssignment> builder)
    {
        builder.ToTable("OfficeAssignment");

        builder.HasKey(e => e.InstructorID);

        builder.Property(e => e.InstructorID)
            .HasColumnName("InstructorID")
            .ValueGeneratedNever();

        builder.Property(e => e.Location).HasMaxLength(50);

        builder.HasOne(d => d.Instructor)
            .WithOne(p => p.OfficeAssignment)
            .HasForeignKey<OfficeAssignment>(d => d.InstructorID);
    }
}
