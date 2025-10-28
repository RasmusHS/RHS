namespace RHS.Application.CQRS.DTO;

public abstract class DtoBase
{
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }
}