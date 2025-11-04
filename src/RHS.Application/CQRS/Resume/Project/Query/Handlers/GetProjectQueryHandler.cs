using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Resume.Project.Query.Handlers;

public class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, QueryProjectDto>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task<Result<QueryProjectDto>> Handle(GetProjectQuery query, CancellationToken cancellationToken = default)
    {
        // TODO: Implement the logic to retrieve a single project by ID
        throw new NotImplementedException();
    }
}