using System.Reflection;
using System.Threading.Channels;

namespace lproject.Mediator.Mediator;

public class WrapperNotify(MethodInfo invokeMethods, IReadOnlyList<Type> handlers)
{
    public Task Invoke(object request, CancellationToken ct = default)
    {
        var task = new List<Task>();
        foreach (var handler in handlers)
        {
            task.Add((Task)invokeMethods.Invoke(handler, [request, ct])!);
        }
        return Task.WhenAll(task);
    }
}