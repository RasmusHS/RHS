using FluentValidation;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.DTO.Project.Command;

public record DeleteProjectDto 
{
    public DeleteProjectDto(Guid id, DateTime created, DateTime lastModified)
    {
        Id = id;
        
        Created = created;
        LastModified = lastModified;
    }
    
    public DeleteProjectDto() { }
    
    public Guid Id { get; set; } // Project Id
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }

    public class Validator : AbstractValidator<DeleteProjectDto>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(Id)).Code);
        }
    }
}