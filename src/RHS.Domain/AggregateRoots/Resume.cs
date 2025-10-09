using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Entities;
using RHS.Domain.ValueObjects;

namespace RHS.Domain.AggregateRoots;

public class Resume : AggregateRoot
{
    // Constructors
    internal Resume() { } // For ORM

    /// <summary>
    /// Constructor to create Resume without initial WorkExperience
    /// </summary>
    /// <param name="introduction"></param>
    /// <param name="fullName"></param>
    /// <param name="address"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="email"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="gitHubLink"></param>
    /// <param name="linkedInLink"></param>
    /// <param name="portfolioLink"></param>
    /// <param name="interest"></param>
    /// <param name="photo"></param>
    public Resume(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string portfolioLink, string interest, byte[] photo)
    {
        Ensure.That(introduction, nameof(introduction)).IsNotNullOrEmpty();
        Ensure.That(fullName, nameof(fullName)).IsNotNull();
        Ensure.That(address, nameof(address)).IsNotNull();
        Ensure.That(phoneNumber, nameof(phoneNumber)).IsNotNull();
        Ensure.That(email, nameof(email)).IsNotNull();
        Ensure.That(dateOfBirth, nameof(dateOfBirth));
        Ensure.That(gitHubLink, nameof(gitHubLink)).IsNotNullOrEmpty();
        Ensure.That(linkedInLink, nameof(linkedInLink)).IsNotNullOrEmpty();
        Ensure.That(portfolioLink, nameof(portfolioLink)).IsNotNullOrEmpty();
        Ensure.That(interest, nameof(interest)).IsNotNullOrEmpty();
        Ensure.That(photo, nameof(photo)).IsNotNull();
        
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
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    /// <summary>
    /// Constructor to create Resume with initial WorkExperience
    /// </summary>
    /// <param name="introduction"></param>
    /// <param name="fullName"></param>
    /// <param name="address"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="email"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="gitHubLink"></param>
    /// <param name="linkedInLink"></param>
    /// <param name="portfolioLink"></param>
    /// <param name="interest"></param>
    /// <param name="photo"></param>
    /// <param name="experience"></param>
    public Resume(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string portfolioLink, string interest, byte[] photo, WorkExperience experience)
    {
        Ensure.That(introduction, nameof(introduction)).IsNotNullOrEmpty();
        Ensure.That(fullName, nameof(fullName)).IsNotNull();
        Ensure.That(address, nameof(address)).IsNotNull();
        Ensure.That(phoneNumber, nameof(phoneNumber)).IsNotNull();
        Ensure.That(email, nameof(email)).IsNotNull();
        Ensure.That(dateOfBirth, nameof(dateOfBirth));
        Ensure.That(gitHubLink, nameof(gitHubLink)).IsNotNullOrEmpty();
        Ensure.That(linkedInLink, nameof(linkedInLink)).IsNotNullOrEmpty();
        Ensure.That(portfolioLink, nameof(portfolioLink)).IsNotNullOrEmpty();
        Ensure.That(interest, nameof(interest)).IsNotNullOrEmpty();
        Ensure.That(photo, nameof(photo)).IsNotNull();
        
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
        
        AddWorkExperience(experience);
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    // Methods
    public void Update(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, Email email, string gitHubLink, string linkedInLink, string portfolioLink, string interest, byte[] photo)
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
    
    public void AddWorkExperience(WorkExperience workExperience)
    {
        Ensure.That(workExperience, nameof(workExperience)).IsNotNull();
        WorkExperience.Add(workExperience);
        
        LastModified = DateTime.Now;
    }
    
    public void AddProject(Project project)
    {
        Ensure.That(project, nameof(project)).IsNotNull();
        Projects.Add(project);
        
        LastModified = DateTime.Now;
    }
    
    public void AddLanguage(Language language)
    {
        Ensure.That(language, nameof(language)).IsNotNull();
        Languages.Add(language);
        
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
    public string PortfolioLink { get; private set; }
    public string Interest { get; private set; }
    public byte[] Photo { get; private set; }
    
    // Navigation properties
    public List<WorkExperience> WorkExperience { get; private set; } // 1-many
    public List<Project> Projects { get; private set; } // 1-many
    public List<Language> Languages { get; private set; } // 1-many
    
    public List<ResumeEdu> Education { get; private set; } // many-many
    public List<ResumeSkills> Skills { get; private set; } // many-many
    public List<ResumeCerts> Certs { get; private set; } // many-many
}