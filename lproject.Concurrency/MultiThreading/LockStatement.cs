namespace lproject.Concurrency.MultiThreading;

public static class LockStatement
{
    public static void MultipleThreadAccessSharedDataByLock()
    {
        // Lock class is an object that it’s obviously a lock
        // in net8 and earlier, use 'object' instead of.
        var lockObject = new Lock();
        int count = 0;
        var threads = new List<Thread>();
        for (int i = 1; i <= 100; i++)
        {
            var thread = new Thread(() =>
            {
                lock (lockObject)
                {
                    count += 1;
                }
            });
            thread.Start();
            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine(count);
    }
}