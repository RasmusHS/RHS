using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Resume.Project.Query.Handlers;

public class GetAllProjectsQueryHandler : IQueryHandler<GetAllProjectsQuery, CollectionResponseBase<QueryProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    
    public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public Task<Result<CollectionResponseBase<QueryProjectDto>>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken = default)
    {
        // TODO: Implement the logic to retrieve all projects
        throw new NotImplementedException();
    }
}