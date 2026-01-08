using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Resume.Entities;

namespace RHS.Application.CQRS.Command.Resume.Handlers;

public class UpdateResumeCommandHandler : ICommandHandler<UpdateResumeCommand>
{
    private readonly IResumeRepository _resumeRepository;
    
    public UpdateResumeCommandHandler(IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;
    }
    
    public async Task<Result> Handle(UpdateResumeCommand command, CancellationToken cancellationToken = default)
    {
        var resumeResult = await _resumeRepository.GetByIdAsync(command.Id) ?? throw new KeyNotFoundException($"Resume with Id {command.Id} was not found.");
        
        resumeResult.Update(
            command.Introduction,
            command.FirstName,
            command.LastName,
            command.Street,
            command.ZipCode,
            command.City,
            command.Email,
            command.GitHubLink,
            command.LinkedInLink,
            command.Photo
        );
        
        await _resumeRepository.UpdateAsync(resumeResult, cancellationToken);
        
        return Result.Ok();
    }
}