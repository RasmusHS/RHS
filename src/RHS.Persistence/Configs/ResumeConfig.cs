using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Resume;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs;

public class ResumeConfig : IEntityTypeConfiguration<Resume>
{
    public void Configure(EntityTypeBuilder<Resume> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(
            resume => resume.Value, 
            value => ResumeId.Create().Value!);

        builder.HasMany(r => r)
            .WithOne()
            .HasForeignKey(fk => fk);
        
        builder.HasMany(r => r)
            .WithOne()
            .HasForeignKey(fk => fk);
        
        builder.HasMany(r => r)
            .WithOne()
            .HasForeignKey(fk => fk);
        
        // Many-to-many via join entity
        builder.HasMany(r => r)
            .WithOne()
            .HasForeignKey(fk => fk);
        
        builder.HasMany(r => r)
            .WithOne()
            .HasForeignKey(fk => fk);
        
        builder.HasMany(r => r)
            .WithOne()
            .HasForeignKey(fk => fk);
    }
}