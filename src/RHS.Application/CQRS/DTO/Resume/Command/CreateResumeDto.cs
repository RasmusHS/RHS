using RHS.Application.CQRS.DTO.Resume.Education.Command;
using RHS.Application.CQRS.DTO.Resume.Language.Command;
using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Application.CQRS.DTO.Resume.WorkExperience.Command;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Command;

public record CreateResumeDto 
{
    public CreateResumeDto(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, 
        Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string? portfolioLink, 
        string interest, byte[] photo, List<CreateLanguageDto> languages, List<CreateWorkExperienceDto> workExperiences, List<CreateProjectDto> projects, 
        List<CreateEducationDto> education, List<CreateResumeSkillsDto> skills, List<CreateResumeCertsDto> certs)
    {
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
    }

    public CreateResumeDto() { }
    
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
    public List<CreateLanguageDto> Languages { get; set; }
    public List<CreateWorkExperienceDto> WorkExperiences { get;  set; }
    public List<CreateProjectDto> Projects { get; set; }
    public List<CreateEducationDto> Education { get; set; }
    public List<CreateResumeSkillsDto> Skills { get; set; }
    public List<CreateResumeCertsDto> Certs { get; set; }
}