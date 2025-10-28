namespace RHS.Application.Data;

public class CollectionResponseBase<T>
{
    public IEnumerable<T> Data { get; set; }
}