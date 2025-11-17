namespace lproject.Mediator.Mediator;

public interface INotifyHandler<T>
where T : INotify
{
    Task HandleAsync(T notification, CancellationToken cancellationToken = default);
}