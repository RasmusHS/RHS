using FluentValidation;
using RHS.Domain.Common;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.DTO.Resume.Project.Command;

public record DeleteProjectDto : DtoBase
{
    public DeleteProjectDto(Guid id, DateTime created, DateTime lastModified)
    {
        Id = id;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public DeleteProjectDto() { }
    
    public Guid Id { get; set; } // Project Id

    public class Validator : AbstractValidator<DeleteProjectDto>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Id)).Code);
        }
    }
}