using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Query;

public record QueryResumeDto : DtoBase
{
    public QueryResumeDto(ResumeId id, string introduction, FullName fullName, Address address, Email email, 
        string gitHubLink, string linkedInLink, byte[] photo, List<QueryProjectDto> projects, DateTime created, DateTime lastModified)
    {
        Id = id;
        
        Introduction = introduction;
        FullName = fullName;
        Address = address;
        Email = email;
        GitHubLink = gitHubLink;
        LinkedInLink = linkedInLink;
        Photo = photo;
        
        Projects = projects;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryResumeDto() { }
    
    public ResumeId Id { get; set; }
    public string Introduction { get; set; }
    public FullName FullName { get; set; }
    public Address Address { get; set; }
    public Email Email { get; set; }
    public string GitHubLink { get; set; }
    public string LinkedInLink { get; set; }
    public byte[] Photo { get; set; }
    public List<QueryProjectDto> Projects { get; set; }
}