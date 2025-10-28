using EnsureThat;
using RHS.Domain.Certificate;
using RHS.Domain.Certificate.ValueObjects;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class ResumeCerts : JoinEntity<ResumeId, CertId>
{
    internal ResumeCerts() { } // For ORM

    private ResumeCerts(ResumeId resumeId, CertId certId, DateTime issueDate, DateTime? expireDate) : base(resumeId, certId)
    {
        Id1 = resumeId;
        Id2 = certId;
        IssueDate = issueDate;
        ExpireDate = expireDate;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<ResumeCerts> Create(ResumeId resumeId, CertId certId, DateTime issueDate, DateTime? expireDate)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(certId, nameof(certId)).IsNotNull();
        Ensure.That(issueDate, nameof(issueDate));
        Ensure.That(expireDate, nameof(expireDate));
        
        return Result.Ok<ResumeCerts>(new ResumeCerts(resumeId, certId, issueDate, expireDate));
    }
    
    public ResumeId ResumeId { get; private set; }
    public CertId CertId { get; private set; }
    public DateTime IssueDate { get; private set; } 
    public DateTime? ExpireDate { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public Cert Cert { get; private set; }
}