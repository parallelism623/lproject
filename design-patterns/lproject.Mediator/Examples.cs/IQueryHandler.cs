using lproject.Mediator.Mediator;

namespace lproject.Mediator.Examples.cs;

public interface IQueryHandler<in T, TResponse> : IRequestHandler<T, TResponse>
where T : IRequest<TResponse>
{ }