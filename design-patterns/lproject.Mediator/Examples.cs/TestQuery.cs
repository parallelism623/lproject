using lproject.Mediator.Mediator;

namespace lproject.Mediator.Examples.cs;

public record TestQuery(string Message): IRequest<string>;