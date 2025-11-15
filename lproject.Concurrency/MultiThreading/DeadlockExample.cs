namespace lproject.Concurrency.MultiThreading;

public static class DeadlockExample
{
    private static Lock _globalLockObject = new Lock();

    static void ThreadB()
    {
        var thread = new Thread(() =>
        {
            lock (_globalLockObject)
            {
                Console.WriteLine("Thread B");
            }
        });
        thread.Start();
        thread.Join();
    }
    public static void ThreadA()
    {
        lock (_globalLockObject)
        {
            ThreadB();
            Console.WriteLine("Thread A");
        }
    }
}