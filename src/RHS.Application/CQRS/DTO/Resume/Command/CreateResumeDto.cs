using FluentValidation;
using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Domain.Common;
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

    public class Validator : AbstractValidator<CreateResumeDto>
    {
        public Validator(bool containsProjects)
        {
            if (containsProjects == true)
            {
                RuleFor(x => x.Introduction).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Introduction)).Code);
                RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(FirstName)).Code);
                RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(LastName)).Code);
                RuleFor(x => x.Street).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Street)).Code);
                RuleFor(x => x.ZipCode).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(ZipCode)).Code);
                RuleFor(x => x.City).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(City)).Code);
                RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Email)).Code);
                RuleFor(x => x.Photo).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Photo)).Code);
                RuleForEach(x => x.Projects).SetValidator(new CreateProjectDto.Validator());
            }
            else
            {
                RuleFor(x => x.Introduction).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Introduction)).Code);
                RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(FirstName)).Code);
                RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(LastName)).Code);
                RuleFor(x => x.Street).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Street)).Code);
                RuleFor(x => x.ZipCode).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(ZipCode)).Code);
                RuleFor(x => x.City).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(City)).Code);
                RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Email)).Code);
                RuleFor(x => x.Photo).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Photo)).Code);
            }
        }
    }
}