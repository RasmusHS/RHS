using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RHS.Application.Data;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}