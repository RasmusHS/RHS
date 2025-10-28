using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => ProjectId.Create().Value!);
        
        builder.HasOne(p => p.Resume)
            .WithMany(r => r.Projects)
            .HasForeignKey(p => p.ResumeId);
    }
}