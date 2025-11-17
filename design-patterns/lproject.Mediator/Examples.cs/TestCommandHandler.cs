namespace lproject.Mediator.Examples.cs;

public class TestCommandHandler : ICommandHandler<TestCommand>
{
    public Task HandleAsync(TestCommand request, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(request.Message);
        return Task.CompletedTask;
    }
}