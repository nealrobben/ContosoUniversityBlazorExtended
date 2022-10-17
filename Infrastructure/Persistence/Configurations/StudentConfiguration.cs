using ContosoUniversityBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityBlazor.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Student");

        builder.Property(e => e.ID).HasColumnName("ID");

        builder.Property(e => e.FirstMidName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasMaxLength(50);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ProfilePictureName)
            .HasMaxLength(200);
    }
}
