using lproject.Mediator.Mediator;

namespace lproject.Mediator.Examples.cs;

public interface IQuery<TResponse> : IRequest<TResponse>
{ }