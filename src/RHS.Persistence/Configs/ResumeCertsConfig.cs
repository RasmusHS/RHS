using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Certificate.ValueObjects;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Persistence.Configs;

public class ResumeCertsConfig : IEntityTypeConfiguration<ResumeCerts>
{
    public void Configure(EntityTypeBuilder<ResumeCerts> builder)
    {
        builder.HasKey(rc => new { rc.ResumeId, rc.CertId });
        builder.Property(rc => rc.ResumeId).HasConversion(
            resumeId => resumeId.Value,
            value => ResumeId.Create().Value!);
        builder.Property(rc => rc.CertId).HasConversion(
            certId => certId.Value,
            value => CertId.Create().Value!);

        builder.HasOne(rc => rc.Resume)
            .WithMany(r => r.Certs)
            .HasForeignKey(rc => rc.ResumeId);

        builder.HasOne(rc => rc.Cert)
            .WithMany(c => c.Resumes)
            .HasForeignKey(rc => rc.CertId);

        //builder.HasOne()
        //    .WithMany()
        //    .HasForeignKey();
    }
}