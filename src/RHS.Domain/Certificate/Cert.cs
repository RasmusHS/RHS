using RHS.Domain.Common;
using RHS.Domain.Entities;
using RHS.Domain.Resume.Entities;

namespace RHS.Domain.Certificate;

public class Cert : Entity
{
    internal Cert() { } // For ORM

    public Cert(/*int certId,*/ string certName, string issuingOrganization, DateTime issueDate)
    {
        //Id = certId;
        CertName = certName;
        IssuingOrganization = issuingOrganization;
        IssueDate = issueDate;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public string CertName { get; private set; }
    public string IssuingOrganization { get; private set; }
    public DateTime IssueDate { get; private set; } // TODO: Move to ResumeCerts and add ExpiryDate there
    
    // Navigation properties
    public List<ResumeCerts> Resumes { get; private set; } // many-many
}