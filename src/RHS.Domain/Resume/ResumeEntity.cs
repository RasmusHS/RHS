using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume;

public sealed class ResumeEntity : AggregateRoot<ResumeId>
{
    // Constructors
    internal ResumeEntity() { } // For ORM

    private ResumeEntity(ResumeId id, string introduction, FullName fullName, Address address, Email email, 
        string gitHubLink, string linkedInLink, byte[] photo)
        : base(id)
    {
        Id = id;
        Introduction = introduction;
        FullName = fullName;
        Address = address;
        Email = email;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        Photo = photo;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }

    public static Result<ResumeEntity> Create(string introduction, FullName fullName, Address address, 
        Email email, string gitHubLink, string linkedInLink, byte[] photo)
    {
        Ensure.That(introduction, nameof(introduction)).IsNotNullOrEmpty();
        Ensure.That(fullName, nameof(fullName)).IsNotNull();
        Ensure.That(address, nameof(address)).IsNotNull();
        Ensure.That(email, nameof(email)).IsNotNull();
        Ensure.That(gitHubLink, nameof(gitHubLink)).IsNotNullOrEmpty();
        Ensure.That(linkedInLink, nameof(linkedInLink)).IsNotNullOrEmpty();
        Ensure.That(photo, nameof(photo)).IsNotNull();
        
        return Result.Ok<ResumeEntity>(new ResumeEntity(ResumeId.Create(), introduction, fullName, address, email, gitHubLink, linkedInLink, photo));
    }

    public void AddProject(ProjectEntity project)
    {
        Ensure.That(project, nameof(project)).IsNotNull();
        _projects.Add(project);
    }

    public void AddRangeProjects(List<ProjectEntity> projects)
    {
        Ensure.That(projects, nameof(projects)).IsNotNull();
        _projects.AddRange(projects);
    }
    
    public void Update(string introduction, string firstName, string lastName, string street, string zipCode, string city, string email, string gitHubLink, string linkedInLink, byte[] photo)
    {
        Ensure.That(introduction, nameof(introduction)).IsNotNullOrEmpty();
        Ensure.That(firstName, nameof(firstName)).IsNotNullOrEmpty();
        Ensure.That(lastName, nameof(lastName)).IsNotNullOrEmpty();
        Ensure.That(street, nameof(street)).IsNotNullOrEmpty();
        Ensure.That(zipCode, nameof(zipCode)).IsNotNullOrEmpty();
        Ensure.That(city, nameof(city)).IsNotNullOrEmpty();
        Ensure.That(email, nameof(email)).IsNotNullOrEmpty();
        Ensure.That(gitHubLink, nameof(gitHubLink)).IsNotNullOrEmpty();
        Ensure.That(linkedInLink, nameof(linkedInLink)).IsNotNullOrEmpty();
        Ensure.That(photo, nameof(photo)).IsNotNull();
        
        Introduction = introduction;
        FullName = FullName.Create(firstName, lastName).Value;
        Address = Address.Create(street, zipCode, city).Value;
        Email = Email.Create(email).Value;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        Photo = photo;
        
        LastModified = DateTime.Now;
    }
    
    // Properties
    public string Introduction { get; private set; }
    public FullName FullName { get; private set; }
    public Address Address { get; private set; }
    public Email Email { get; private set; }
    public string GitHubLink { get; private set; }
    public string LinkedInLink { get; private set; }
    public byte[] Photo { get; private set; }
    
    // Aggregate members - Navigation properties
    private readonly List<ProjectEntity> _projects = new();
    public IReadOnlyList<ProjectEntity> Projects => _projects.AsReadOnly(); // one-to-many
}