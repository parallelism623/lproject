namespace lproject.Concurrency.MultiThreading;

public static class TaskRunExample
{
    public static async Task RunInBackground()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < 10; ++i)
        {
            var icopy = i;

            tasks.Add(Task.Run(() =>
            {
                Console.WriteLine($"Hello from thread {icopy}");
            }));
        }

        await Task.WhenAll(tasks);
    }
}