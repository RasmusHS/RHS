using MediatR;
using RHS.Domain.Common;

namespace RHS.Application.Data;

public interface IQuery<T> : IRequest<Result<T>>
{
}