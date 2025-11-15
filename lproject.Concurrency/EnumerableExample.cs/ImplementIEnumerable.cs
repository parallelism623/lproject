using System.Collections;
using System.Runtime.CompilerServices;

namespace lproject.Concurrency.EnumerableExample.cs;

public static class ImplementIEnumerable
{
        private static async IAsyncEnumerable<int> AsyncYieldDemo(
            [EnumeratorCancellation] System.Threading.CancellationToken cancellationToken = default)
    {
        yield return 1;
        await Task.Delay(1000, cancellationToken);
        yield return 2;
    }

    public static async Task UseYieldDemo()
    {
        var cancel = new CancellationTokenSource();
        cancel.Cancel();
        var seq = AsyncYieldDemo();
        Task.Run(async () =>
        {
            await foreach (var item in seq)
            {
                Console.WriteLine(item);
            }
        });
        await foreach (var item in seq.WithCancellation(cancel.Token))
        {
            Console.WriteLine(item);    
        }
    }
}