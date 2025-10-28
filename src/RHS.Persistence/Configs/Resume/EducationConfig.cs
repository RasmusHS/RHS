using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Institution.ValueObjects;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs.Resume;

public class EducationConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(e => new { e.ResumeId, e.Degree });
        builder.Property(e => e.ResumeId).HasConversion(
            resumeId => resumeId.Value,
            value => ResumeId.Create().Value!);
        builder.Property(e => e.Degree).HasConversion(
            degree => degree.Value,
            value => Degree.Create(value).Value!);

        builder.HasOne(e => e.Resume)
            .WithMany(r => r.Education)
            .HasForeignKey(e => e.ResumeId);

        builder.HasOne(e => e.Institution)
            .WithMany(i => i.Resumes)
            .HasForeignKey(e => e.Degree);
    }
}