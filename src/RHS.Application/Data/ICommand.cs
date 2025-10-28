using MediatR;
using RHS.Domain.Common;

namespace RHS.Application.Data;

public interface ICommand : IRequest<Result>
{
}