using RHS.Application.CQRS.DTO.Resume.Education.Query;
using RHS.Application.CQRS.DTO.Resume.Language.Query;
using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.CQRS.DTO.Resume.WorkExperience.Query;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Query;

public class QueryResumeDto : DtoBase
{
    public QueryResumeDto(ResumeId id,string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, 
        Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string? portfolioLink, 
        string interest, byte[] photo, List<QueryLanguageDto> languages, List<QueryWorkExperienceDto> workExperiences, List<QueryProjectDto> projects, 
        List<QueryEducationDto> education, List<QueryResumeSkillsDto> skills, List<QueryResumeCertsDto> certs, DateTime created, DateTime lastModified)
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
        
        Languages = languages;
        WorkExperiences = workExperiences;
        Projects = projects;
        Education = education;
        Skills = skills;
        Certs = certs;
        
        Created = created;
        LastModified = lastModified;
    }

    public QueryResumeDto() { }
    
    public ResumeId Id { get; set; }
    public string Introduction { get; set; }
    public FullName FullName { get; set; }
    public Address Address { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Email Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string GitHubLink { get; set; }
    public string LinkedInLink { get; set; }
    public string? PortfolioLink { get; set; }
    public string Interest { get; set; }
    public byte[] Photo { get; set; }
    public List<QueryLanguageDto> Languages { get; set; }
    public List<QueryWorkExperienceDto> WorkExperiences { get;  set; }
    public List<QueryProjectDto> Projects { get; set; }
    public List<QueryEducationDto> Education { get; set; }
    public List<QueryResumeSkillsDto> Skills { get; set; }
    public List<QueryResumeCertsDto> Certs { get; set; }
}