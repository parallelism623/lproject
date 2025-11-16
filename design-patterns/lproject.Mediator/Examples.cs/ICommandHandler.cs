using lproject.Mediator.Mediator;

namespace lproject.Mediator.Examples.cs;

public interface ICommandHandler<in T> : IRequestHandler<T>
where T : ICommand
{
    
}