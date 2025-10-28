using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class ResumeConfig : IEntityTypeConfiguration<Domain.Resume.Resume>
{
    public void Configure(EntityTypeBuilder<Domain.Resume.Resume> builder)
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
        
        builder.OwnsOne(r => r.PhoneNumber, propertyBuilder =>
        {
            propertyBuilder.Property(p => p.CountryCode);
            propertyBuilder.Property(p => p.Number);
        });

        builder.Property(r => r.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value, true).Value!);
        
        // builder.Property(r => r).HasConversion(
        // );
        
        // builder.OwnsOne(r => r, propertyBuilder =>
        // {
        //     propertyBuilder.Property();
        // });
        
        // One-to-many relationships
        builder.HasMany(r => r.Languages) // Language
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
        
        builder.HasMany(r => r.WorkExperiences) // WorkExperience
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
        
        builder.HasMany(r => r.Projects) // Project
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
        
        // Many-to-many via join entity
        builder.HasMany(r => r.Education) // EducationConfig
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
        
        builder.HasMany(r => r.Skills) // Skills
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
        
        builder.HasMany(r => r.Certs) // Certs
            .WithOne()
            .HasForeignKey(fk => fk.ResumeId);
    }
}