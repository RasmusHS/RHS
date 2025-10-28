using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Certificate;
using RHS.Domain.Certificate.ValueObjects;

namespace RHS.Persistence.Configs;

public class CertConfig : IEntityTypeConfiguration<Cert>
{
    public void Configure(EntityTypeBuilder<Cert> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            id => id.Value,
            value => CertId.Create().Value!);
    }
}