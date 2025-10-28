using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class LanguageConfig : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasConversion(
            id => id.Value,
            value => LanguageId.Create().Value!);
        
        builder.HasOne(l => l.Resume)
            .WithMany(r => r.Languages)
            .HasForeignKey(l => l.ResumeId);
    }
}