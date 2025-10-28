using RHS.Domain.Certificate.ValueObjects;

namespace RHS.Application.CQRS.DTO.Cert.Query;

public class QueryCertDto : DtoBase
{
    public QueryCertDto(CertId id, string certName, string issuingOrganization, DateTime created, DateTime lastModified)
    {
        Id = id;
        CertName = certName;
        IssuingOrganization = issuingOrganization;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryCertDto() { }
    
    public CertId Id { get; set; }
    public string CertName { get; set; }
    public string IssuingOrganization { get; set; }
}