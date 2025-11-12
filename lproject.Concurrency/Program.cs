using lproject.Concurrency.MultiThreading;
using System.Collections;



Console.WriteLine("Start Main Thread");
List<int> values = [1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14];
SendMailItemsInParrallel.SendMailItemInMultipleThreadingByParallelClass(values);   
Console.WriteLine("Stop Main Thread");


// sleep prevent IHost shutdown.
await Task.Delay(2000);

