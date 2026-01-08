using FluentValidation;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Project.Command;

public record UpdateProjectDto 
{
    public UpdateProjectDto(Guid id, Guid resumeId, string projectTitle, string description, string projectUrl, byte[] demoGif, bool isFeatured, DateTime created, DateTime lastModified)
    {
        Id = id;
        ResumeId = resumeId;
        
        ProjectTitle = projectTitle;
        Description = description;
        ProjectUrl = projectUrl;
        DemoGif = demoGif;
        IsFeatured = isFeatured;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public UpdateProjectDto() { }
    
    public Guid Id { get; set; } // Project Id
    public Guid ResumeId { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public string ProjectUrl { get; set; }
    public byte[] DemoGif { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }

    public class Validator : AbstractValidator<UpdateProjectDto>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Id)).Code);
            RuleFor(x => x.ResumeId).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(ResumeId)).Code);
            RuleFor(x => x.ProjectTitle).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(ProjectTitle)).Code);
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(Description)).Code);
            RuleFor(x => x.ProjectUrl).NotNull().NotEmpty().WithMessage(Errors.General.ValueIsRequired(nameof(ProjectUrl)).Code);
            RuleFor(x => x.DemoGif).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(DemoGif)).Code);
        }
    }
}