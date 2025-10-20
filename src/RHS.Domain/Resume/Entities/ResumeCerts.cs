using RHS.Domain.Certificate;
using RHS.Domain.Certificate.ValueObjects;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class ResumeCerts : JoinEntity<ResumeId, CertId>
{
    //internal ResumeCerts() { } // For ORM

    private ResumeCerts(ResumeId resumeId, CertId certId) : base(resumeId, certId)
    {
        Id1 = resumeId;
        Id2 = certId;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<ResumeCerts> Create(ResumeId resumeId, CertId certId)
    {
        return Result.Ok<ResumeCerts>(new ResumeCerts(resumeId, certId));
    }
    
    public ResumeId ResumeId { get; private set; }
    public CertId CertId { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public Cert Cert { get; private set; }
}