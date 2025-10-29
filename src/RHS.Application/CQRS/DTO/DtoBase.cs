namespace RHS.Application.CQRS.DTO;

public abstract record DtoBase
{
    public DateTime Created { get; protected set; }
    public DateTime LastModified { get; protected set; }
}