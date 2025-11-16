namespace lproject.Mediator.Examples.cs;

public class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    public Task<string> HandleAsync(TestQuery request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult("Hello World");
    }
}