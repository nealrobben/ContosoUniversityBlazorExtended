using ContosoUniversityBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoUniversityBlazor.Persistence.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructor");

        builder.Property(e => e.ID).HasColumnName("ID");

        builder.Property(e => e.FirstMidName)
            .HasColumnName("FirstName")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ProfilePictureName)
            .HasMaxLength(200);
    }
}
