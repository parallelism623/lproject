using lproject.Mediator.Constants.cs;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace lproject.Mediator.Mediator;
using static ExceptionMessages;
public class WrapperRequest(MethodInfo invokeMethod, Type handlerType)
{
    public async Task<object?> Invoke(object request, IServiceProvider serviceProvider, CancellationToken ct = default) 
    {
        var handler = serviceProvider.GetRequiredService(handlerType) 
                      ?? throw new ArgumentNullException($"Can not find handler by {handlerType.AssemblyQualifiedName}");
        
        var resultObj = invokeMethod.Invoke(handler, [ request, ct ]);

        if (resultObj is Task task)
        {
            await task.ConfigureAwait(false);
            var taskType = task.GetType();
            if (taskType.IsGenericType && taskType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var resultProperty = taskType.GetProperty("Result");
                return resultProperty?.GetValue(task);
            }
            return null;
        }

        return resultObj;
    }

}