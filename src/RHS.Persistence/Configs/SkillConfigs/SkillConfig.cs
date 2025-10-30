using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Skill;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Persistence.Configs.SkillConfigs;

public class SkillConfig : IEntityTypeConfiguration<SkillSet>
{
    public void Configure(EntityTypeBuilder<SkillSet> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasConversion(
            id => id.Value,
            value => SkillId.Create().Value!);
        
        builder.HasMany(s => s.Resumes)
            .WithOne(rs => rs.Skill)
            .HasForeignKey(rs => rs.SkillId);
        
        builder.HasMany(s => s.SubSkills)
            .WithOne(ss => ss.ParentSkill)
            .HasForeignKey(ss => ss.ParentSkillId);
    }
}