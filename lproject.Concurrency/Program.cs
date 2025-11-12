using lproject.Concurrency.Multithreading;
using lproject.Concurrency.MultiThreading;
using System.Collections;



Console.WriteLine("Start Main Thread");
var workQueueItem = new WorkQueuePatternExample();
var thread = new Thread(workQueueItem.Start);
Console.WriteLine("Stop Main Thread");


