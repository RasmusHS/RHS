using RHS.Domain.Common;

namespace RHS.Domain.Entities;

public class Certificate : Entity
{
    internal Certificate()
    {
        
    }

    public Certificate(/*int certId,*/ string certName, string issuingOrganization, DateTime issueDate)
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
    public DateTime IssueDate { get; private set; }
    
    // Navigation properties
    public List<ResumeCerts> Resumes { get; private set; } // many-many
}