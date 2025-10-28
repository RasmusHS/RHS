using EnsureThat;
using RHS.Domain.Certificate.ValueObjects;
using RHS.Domain.Common;
using RHS.Domain.Resume.Entities;

namespace RHS.Domain.Certificate;

public sealed class Cert : AggregateRoot<CertId>
{
    internal Cert() { } // For ORM

    private Cert(CertId id, string certName, string issuingOrganization) : base(id)
    {
        Id = id;
        CertName = certName;
        IssuingOrganization = issuingOrganization;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<Cert> Create(string certName, string issuingOrganization)
    {
        Ensure.That(certName, nameof(certName)).IsNotNullOrEmpty();
        Ensure.That(issuingOrganization, nameof(issuingOrganization)).IsNotNullOrEmpty();
        
        return Result.Ok<Cert>(new Cert(CertId.Create(), certName, issuingOrganization));
    }
    
    public string CertName { get; private set; }
    public string IssuingOrganization { get; private set; }
    
    // Navigation properties
    private readonly List<ResumeCerts> _resumes = new();
    public IReadOnlyList<ResumeCerts> Resumes => _resumes.AsReadOnly(); // Navigation property
}