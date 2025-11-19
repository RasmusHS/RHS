using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Query;

public record QueryResumeDto : DtoBase
{
    public QueryResumeDto(Guid id, string introduction, string firstName, string lastName, string street, string zipCode, string city, string email, 
        string gitHubLink, string linkedInLink, byte[] photo, List<QueryProjectDto> projects, DateTime created, DateTime lastModified)
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
        
        Projects = projects;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryResumeDto() { }
    
    public Guid Id { get; set; }
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
    public List<QueryProjectDto> Projects { get; set; }
}