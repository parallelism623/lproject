namespace lproject.Concurrency.MultiThreading;

public static class CreateNewThreadByClass
{
    public static void RunInBackground() {
        var newThread = new Thread(CodeToRunInBackgroundThread);
        newThread.IsBackground = true;
        newThread.Start();
    }
    
    public static void RunLotsOfThreads() {

        var threads = new Thread[100];
        int counter = 0;
        void MyThread(object? parameter) { Interlocked.Increment(ref counter); }
        for(int i = 0; i < 100; ++i) {
            threads[i] = new Thread(MyThread);
            threads[i].Start(); 
        }
        // Use Thread.Join() to wait until it finish
        // thread.Join will block logic code until thread finish, but 
        // the loop will wait until the longest running of the threads finishes.
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine(counter);
        Console.WriteLine("Thread finished");
    } 
    

    static void CodeToRunInBackgroundThread()
    {
        Thread.Sleep(1000);
        Console.WriteLine("CodeToRunInBackgroundThread");
    }
}