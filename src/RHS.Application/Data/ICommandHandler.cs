using MediatR;
using RHS.Domain.Common;

namespace RHS.Application.Data;

public interface ICommandHandler<in TCommand> 
    : IRequestHandler<TCommand, Result> where TCommand : ICommand
{
    new Task<Result> Handle(TCommand command, CancellationToken cancellationToken = default);
}