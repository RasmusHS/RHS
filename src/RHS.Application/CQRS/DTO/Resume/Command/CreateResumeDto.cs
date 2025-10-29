using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Command;

public record CreateResumeDto : DtoBase
{
    public CreateResumeDto(string introduction, FullName fullName, Address address, PhoneNumber phoneNumber, 
        Email email, DateTime dateOfBirth, string gitHubLink, string linkedInLink, string? portfolioLink, 
        string interest, byte[] photo, List<CreateLanguageDto> languages, List<CreateWorkExperienceDto> workExperiences, List<CreateProjectDto> projects, 
        List<CreateEducationDto> education, List<CreateResumeSkillsDto> skills, List<CreateResumeCertsDto> certs)
    {
        
    }

    public CreateResumeDto() { }
}