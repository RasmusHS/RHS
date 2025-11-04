using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.Data;

namespace RHS.Application.CQRS.Resume.Project.Query;

public class GetAllProjectsQuery : IQuery<CollectionResponseBase<QueryProjectDto>>
{
}