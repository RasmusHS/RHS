using RHS.Domain.AggregateRoots;
using RHS.Domain.Common;

namespace RHS.Domain.Entities;

public class ResumeCerts : JoinEntity
{
    internal ResumeCerts()
    {
        
    }

    public ResumeCerts(int resumeId, int certId)
    {
        ResumeId = resumeId;
        CertId = certId;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public int ResumeId { get; private set; }
    public int CertId { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
    public Certificate Certification { get; private set; }
}