using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class ResumeSkillsConfig : IEntityTypeConfiguration<ResumeSkills>
{
    public void Configure(EntityTypeBuilder<ResumeSkills> builder)
    {
        builder.HasKey(r => new { r.ResumeId, r.SkillId });
        builder.Property(r => r.ResumeId).HasConversion(
            resumeId => resumeId.Value,
            value => ResumeId.Create().Value!);
        builder.Property(r => r.SkillId).HasConversion(
            skillId => skillId.Value,
            value => SkillId.Create().Value!);
        
        builder.HasOne(r => r.Resume)
            .WithMany(resume => resume.Skills)
            .HasForeignKey(r => r.ResumeId);
        
        builder.HasOne(r => r.Skill)
            .WithMany(skill => skill.Resumes)
            .HasForeignKey(r => r.SkillId);
    }
}