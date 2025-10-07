namespace RHS.Domain.Common;

public abstract class JoinEntity
{
    public DateTime LastModified { get; protected set; }
    public DateTime Created { get; protected set; }
}