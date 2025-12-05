using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Resume.Command;

public class UpdateResumeCommand : ICommand
{
    public UpdateResumeCommand(ResumeId id, string introduction, string firstName, string lastName, 
        string street, string zipCode, string city, 
        string email, string gitHubLink, string linkedInLink, 
        byte[] photo, DateTime created, DateTime lastModified)
    {
        Id = id;
        Introduction = introduction;
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        ZipCode = zipCode;
        City = city;
        Email = email;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        Photo = photo;
        
        Created = created;
        LastModified = lastModified;
    }

    public UpdateResumeCommand() { }
    
    public ResumeId Id { get; set; }
    public string Introduction { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string GitHubLink { get; set; }
    public string LinkedInLink { get; set; }
    public byte[] Photo { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}