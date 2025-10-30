using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Skill.Entities;
using RHS.Domain.Skill.ValueObjects;

namespace RHS.Persistence.Configs.SkillConfigs;

public class SubSkillConfig : IEntityTypeConfiguration<SubSkill>
{
    public void Configure(EntityTypeBuilder<SubSkill> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasConversion(
            id => id.Value,
            value => SubSkillId.Create().Value!);
        
        builder.HasOne(s => s.ParentSkill)
            .WithMany(ps => ps.SubSkills)
            .HasForeignKey(s => s.ParentSkillId);
    }
}