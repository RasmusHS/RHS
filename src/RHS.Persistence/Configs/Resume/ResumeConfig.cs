using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class ResumeConfig : IEntityTypeConfiguration<ResumeEntity>
{
    public void Configure(EntityTypeBuilder<ResumeEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(
            resume => resume.Value, 
            value => ResumeId.Create().Value!);
        
        // Properties
        builder.OwnsOne(r => r.FullName, propertyBuilder =>
        {
            propertyBuilder.Property(p => p.FirstName);
            propertyBuilder.Property(p => p.LastName);
        });
        
        builder.OwnsOne(r => r.Address, propertyBuilder =>
        {
            propertyBuilder.Property(p => p.Street);
            propertyBuilder.Property(p => p.ZipCode);
            propertyBuilder.Property(p => p.City);
        });

        builder.Property(r => r.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value, true).Value!);
        
        // One-to-many relationships
        builder.HasMany(r => r.Projects) // ProjectEntity
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
    }
}