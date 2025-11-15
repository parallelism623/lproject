using System.Data.Common;

namespace lproject.Concurrency.MultiThreading;

public static class UseThreadInThreadPool
{
    public static void RunInBackgroundByThreadPool() {
       ThreadPool.QueueUserWorkItem(CodeToRunInBackgroundThread, 10);
        void CodeToRunInBackgroundThread(object? data)
        {
            Console.WriteLine(data);
        }
    }
}