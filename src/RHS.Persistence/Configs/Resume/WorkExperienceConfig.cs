using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class WorkExperienceConfig : IEntityTypeConfiguration<WorkExperience>
{
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id).HasConversion(
            id => id.Value,
            value => WorkExpId.Create().Value!);
        
        builder.HasOne(w => w.Resume)
            .WithMany(r => r.WorkExperiences)
            .HasForeignKey(w => w.ResumeId);
    }
}