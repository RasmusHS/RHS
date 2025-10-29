using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Institution.ValueObjects;

namespace RHS.Application.CQRS.DTO.Institution.Query;

public record QueryInstitutionDto : DtoBase
{
    public QueryInstitutionDto(InstitutionId id, string institutionName, Address location, DateTime created, DateTime lastModified)
    {
        Id = id;
        
        InstitutionName = institutionName;
        Location = location;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public QueryInstitutionDto() { }
    
    public InstitutionId Id { get; set; }
    public string InstitutionName { get; set; } 
    public Address Location { get; set; }
}