using MediatR;

namespace Shared.CQRS;

public interface ICommand : IRequest<Unit> { }
