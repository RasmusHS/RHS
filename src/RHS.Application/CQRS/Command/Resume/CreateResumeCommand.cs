using RHS.Application.CQRS.Command.Project;
using RHS.Application.Data;

namespace RHS.Application.CQRS.Command.Resume;

public class CreateResumeCommand : ICommand
{
    public CreateResumeCommand(string introduction, string firstName, string lastName, 
        string street, string zipCode, string city, 
        string email, string gitHubLink, string linkedInLink, 
        byte[] photo, List<CreateProjectCommand>? projects)
    {
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
        Projects = projects;
    }
    
    public string Introduction { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Street { get; }
    public string ZipCode { get; }
    public string City { get; }
    public string Email { get; }
    public string GitHubLink { get; }
    public string LinkedInLink { get; }
    public byte[] Photo { get; }
    public List<CreateProjectCommand>? Projects { get; }
}