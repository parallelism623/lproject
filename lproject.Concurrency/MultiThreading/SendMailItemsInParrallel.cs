using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
namespace lproject.Concurrency.MultiThreading;


public static class SendMailItemsInParrallel
{
    
    public static void SendMailItems()
    {
        for(int i = 0; i < 10; i++)
        {
            SendMail();
            // Send mail step by step.
        }
    }

    public static void SendMailItemsInMultipleThreading()
    {
        var threads = new List<Thread>();
        for(int i= 0;i  < 10; i++)
        {
            var thread = new Thread(SendMail);
            threads.Add(thread);
            thread.Start();
        }
        threads.ForEach(it => it.Join());   
    }

    public static void SendMailItemInMultipleThreadingByParallelClass(IEnumerable<int> mailItems) 
    {
        var sw = Stopwatch.StartNew();
        var bags = new ConcurrentBag<int>();
        var result = Parallel.ForEach(mailItems, 
            (value, _) =>
            {
                Thread.Sleep(1000);
                bags.Add(value);
            });
        Console.WriteLine(result.IsCompleted.ToString());
        Console.WriteLine(bags.Count);
        Console.WriteLine(sw.ElapsedMilliseconds.ToString());
        sw.Stop();
    }

    public static async Task SendMailItemInMultipleThreadingAsync()
    {
        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            var task = Task.Run(async () =>
            {
                await SendMailAsync();
            });
            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
    }

    private static void SendMail()
    {
    }

    private static Task SendMailAsync()
    {
        return Task.CompletedTask;  
    }
}
