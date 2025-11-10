using lproject.Concurrency.MultiThreading;
using System.Collections;



Console.WriteLine("Start Main Thread");
await TaskRunExample.RunInBackground();   
Console.WriteLine("Stop Main Thread");


// sleep prevent IHost shutdown.
await Task.Delay(2000);

