using MediatR;

namespace Shared.CQRS;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand { }
