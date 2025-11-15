using lproject.Concurrency.EnumerableExample.cs;
using lproject.Concurrency.Multithreading;
using lproject.Concurrency.MultiThreading;
using System.Collections;



Console.WriteLine("Start Main Thread");
await ImplementIEnumerable.UseYieldDemo();
Console.WriteLine("Stop Main Thread");


