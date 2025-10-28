using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Institution.ValueObjects;
using RHS.Domain.Resume.Entities;

namespace RHS.Domain.Institution;

public sealed class InstitutionEntity : AggregateRoot<InstitutionId>
{
    internal InstitutionEntity() { } // For ORM

    private InstitutionEntity(InstitutionId id, string institutionName, Address location) : base(id)
    {
        Id = id;
        InstitutionName = institutionName;
        Location = location;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<InstitutionEntity> Create(InstitutionId id, string institutionName, Address location)
    {
        Ensure.That(institutionName, nameof(institutionName)).IsNotNullOrEmpty();
        Ensure.That(location, nameof(location)).IsNotNull();
        
        return Result.Ok<InstitutionEntity>(new InstitutionEntity(InstitutionId.Create(), institutionName, location));
    }
    
    public string InstitutionName { get; private set; } 
    public Address Location { get; private set; }
    
    // Navigation properties
    private readonly List<Education> _resumes = new();
    public IReadOnlyList<Education> Resumes => _resumes.AsReadOnly(); // many-many with Resume
}