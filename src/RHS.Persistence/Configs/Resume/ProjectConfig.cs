using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class ProjectConfig : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => ProjectId.Create().Value!);
        
        builder.HasOne(p => p.ResumeEntity)
            .WithMany(r => r.Projects)
            .HasForeignKey(p => p.ResumeId);
    }
}