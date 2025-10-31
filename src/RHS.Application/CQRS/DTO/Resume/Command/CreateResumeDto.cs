using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Command;

public record CreateResumeDto 
{
    public CreateResumeDto(string introduction, string firstName, string lastName, 
        string street, string zipCode, string city, 
        string email, string gitHubLink, string linkedInLink, 
        byte[] photo, List<CreateProjectDto>? projects)
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

    public CreateResumeDto() { }
    
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
    public List<CreateProjectDto>? Projects { get; set; }
}