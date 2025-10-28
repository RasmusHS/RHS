using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RHS.Domain.Institution;
using RHS.Domain.Institution.ValueObjects;

namespace RHS.Persistence.Configs;

public class InstitutionConfig : IEntityTypeConfiguration<InstitutionEntity>
{
    public void Configure(EntityTypeBuilder<InstitutionEntity> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasConversion(
            id => id.Value,
            value => InstitutionId.Create().Value!);

        builder.OwnsOne(i => i.Location, propertyBuilder =>
        {
            propertyBuilder.Property(p => p.Street);
            propertyBuilder.Property(p => p.ZipCode);
            propertyBuilder.Property(p => p.City);
        });
    }
}