using RHS.Domain.Common;

namespace RHS.Application.Data;

public interface IDispatcher
{
    Task<Result<T>> Dispatch<T>(IQuery<T> query);
    Task<Result> Dispatch(ICommand command);
}