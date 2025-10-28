using RHS.Domain.Certificate.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Query;

public class QueryResumeCertsDto : DtoBase
{
    public QueryResumeCertsDto(ResumeId resumeId, CertId certId, DateTime issueDate, DateTime? expireDate, DateTime created, DateTime lastModified)
    {
        ResumeId = resumeId;
        CertId = certId;
        
        IssueDate = issueDate;
        ExpireDate = expireDate;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryResumeCertsDto() { }
    
    public ResumeId ResumeId { get; set; }
    public CertId CertId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpireDate { get; set; }
}