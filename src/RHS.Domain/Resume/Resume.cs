using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume;

public sealed class Resume : AggregateRoot<ResumeId>
{
    // Constructors
    internal Resume() { } // For ORM

    private Resume(ResumeId id, string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, 
        Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string? portfolioLink, 
        string interest, byte[] photo, List<Language> languages, List<WorkExperience> workExperiences, List<Project> projects, 
        List<Education> education, List<ResumeSkills> skills, List<ResumeCerts> certs)
        : base(id)
    {
        Id = id;
        Introduction = introduction;
        FullName = fullName;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        DateOfBirth = dateOfBirth;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        PortfolioLink = portfolioLink;
        Interest = interest;
        Photo = photo;
        _languages = languages;
        _workExperiences = workExperiences;
        _projects = projects;
        _education = education;
        _skills = skills;
        _certs = certs;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }

    public static Result<Resume> Create(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, 
        Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string? portfolioLink, 
        string interest, byte[] photo, List<Language> languages, List<WorkExperience> workExperiences, List<Project> projects, 
        List<Education> education, List<ResumeSkills> skills, List<ResumeCerts> certs)
    {
        Ensure.That(introduction, nameof(introduction)).IsNotNullOrEmpty();
        Ensure.That(fullName, nameof(fullName)).IsNotNull();
        Ensure.That(address, nameof(address)).IsNotNull();
        Ensure.That(phoneNumber, nameof(phoneNumber)).IsNotNull();
        Ensure.That(email, nameof(email)).IsNotNull();
        Ensure.That(dateOfBirth, nameof(dateOfBirth));
        Ensure.That(gitHubLink, nameof(gitHubLink)).IsNotNullOrEmpty();
        Ensure.That(linkedInLink, nameof(linkedInLink)).IsNotNullOrEmpty();
        Ensure.That(portfolioLink, nameof(portfolioLink));
        Ensure.That(interest, nameof(interest)).IsNotNullOrEmpty();
        Ensure.That(photo, nameof(photo)).IsNotNull();
        Ensure.That(languages, nameof(languages)).HasItems();
        
        return Result.Ok<Resume>(new Resume(ResumeId.Create(), introduction, fullName, address, phoneNumber, email, dateOfBirth, gitHubLink, linkedInLink, portfolioLink, interest, photo, languages, workExperiences, projects, education, skills, certs));
    }
    
    public void Update(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, Email email, string gitHubLink, string linkedInLink, string? portfolioLink, string interest, byte[] photo)
    {
        Introduction = introduction;
        FullName = fullName;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        PortfolioLink = portfolioLink;
        Interest = interest;
        Photo = photo;
        
        LastModified = DateTime.Now;
    }
    
    // Properties
    public string Introduction { get; private set; }
    public FullName FullName { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string GitHubLink { get; private set; }
    public string LinkedInLink { get; private set; }
    public string? PortfolioLink { get; private set; }
    public string Interest { get; private set; }
    public byte[] Photo { get; private set; }
    
    // Aggregate members - Navigation properties
    private readonly List<Language> _languages = new();
    public IReadOnlyList<Language> Languages => _languages.AsReadOnly(); // one-to-many
    
    private readonly List<WorkExperience> _workExperiences = new();
    public IReadOnlyList<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly(); // one-to-many
    
    private readonly List<Project> _projects = new();
    public IReadOnlyList<Project> Projects => _projects.AsReadOnly(); // one-to-many
    
    private readonly List<Education> _education = new();
    public IReadOnlyList<Education> Education => _education.AsReadOnly(); // many-many with InstitutionEntity
    
    private readonly List<ResumeSkills> _skills = new();
    public IReadOnlyList<ResumeSkills> Skills => _skills.AsReadOnly(); // many-many
    
    private readonly List<ResumeCerts> _certs = new();
    public IReadOnlyList<ResumeCerts> Certs => _certs.AsReadOnly(); // many-many
}