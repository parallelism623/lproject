**Multithreading** is a model that allows a single process to have multiple threads executing concurrently.

In a single-core system, the CPU can switch between threads to handle different tasks, giving the *illusion* that they are running simultaneously.

When multiple CPU cores are available (multi-core), each core can process one thread independently, allowing multiple threads to **actually run in parallel**.

Each thread exists in certain states such as **Running**, **Ready**, **Blocked**, or **Terminated**.

At any given time, a **CPU core** will select one thread in the **Ready** state to execute (switch to **Running**).

The thread continues to run until completion or until it encounters a situation that causes it to **pause** (Blocked) — such as reading/writing files, waiting for network data, or performing I/O operations in general.

These operations that cause a thread to pause are called **blocking operations**.

When a thread is blocked, it “sleeps” until the operating system notifies that the external operation (I/O, timer, network, etc.) is completed.

Each thread has its own **local stack memory**, and switching between threads (**context switching**) incurs CPU overhead.

If a program has many blocking operations and each operation holds a separate thread, many threads will remain idle — not executing but still consuming resources.

When hundreds or thousands of I/O tasks are waiting, the system may need to create thousands of threads → leading to overload, consuming RAM and CPU → the application may **slow down or crash**.

To solve this problem, **asynchronous programming** was introduced as a mechanism to **free threads** from waiting.

When encountering a blocking operation, the thread doesn’t need to wait but can **return control** to the operating system to perform other tasks.

When the operation is completed, the operating system (or runtime) **signals an event** to notify the program, and an available thread (usually from a thread pool) will **process the continuation** of that task.

Thanks to this mechanism, a program can **combine the benefits of multithreading and async programming**:

- Multithreading allows **executing multiple logical tasks at once** (CPU-bound or I/O-bound).
- Async programming helps **reduce resource waste** by not keeping threads stuck on I/O tasks.

In practice, when a blocking operation occurs, the program (if it doesn’t depend on that operation’s result) can **continue executing other tasks**, pausing only when the result is actually needed — leading to better performance, responsiveness, and scalability.

## Parallel Execution

### Thread

When a process starts, it begins with a main thread running on the Main method (it may have some system threads alongside).

Additional threads can be created to perform desired tasks. In C#, the Thread class represents a thread, used as follows:

```csharp
var thread = new Thread(delegate);
// Delegate is a void function with an optional object parameter.
thread.Start(object);
// Pass parameter to delegate.
```

Constructing and destructing thread objects consumes significant system resources, so creating and destroying them repeatedly may lead to more time spent managing threads rather than executing assigned work.

Therefore, it’s ideal to use Thread.Start for long-running tasks, and not for async/await or short tasks.

### Thread pool

A thread pool contains a number of pre-created threads for short-running tasks. These tasks take turns using threads from the pool without creating or destroying thread objects. It’s not recommended to modify the settings of thread pool threads or related components.

```csharp
ThreadPool.QueueUserWorkItem(delegate, object? paramOfDelegate);
```

### Task.Run

Task.Run(task) also uses the ThreadPool to minimize thread allocation/deallocation. Since Tasks are generally short-running, most of their workload is performed by components outside the CPU.

```csharp
Task.Run(Task);
Task.Run(() => Task);
```

### Accessing Data From Multiple Threads

In some cases, local data can be shared across multiple threads, leading to race conditions.

We can make data immutable, use the Mutex class (representing an OS-level mutex), or use the ***lock*** statement.

The lock statement is a lightweight internal .NET implementation, faster than the Mutex class since it doesn’t require a system call. While immutability fits functional programming, in typical C# development, the ***lock*** statement is usually preferred.

```csharp
var objectLock = new object();
lock (objectLock) { ++theValue; }
```

Each ***lock*** statement requires a lock object and a following code block. When execution enters the code block, the object is locked, and once it exits, the object is released.

Deadlocks are an important issue in threading and locking. In general, a deadlock occurs when one or more threads wait for something that will never happen. The simplest case: thread A waits for thread B to complete, while thread B waits for thread A.

Lock objects should typically be private for safety and easier control, reducing deadlock risks.

### Thread Synchronization

Thread synchronization is the technique of coordinating threads to share data or actions without causing race conditions.

Besides the lock statement, C# supports ManualResetEventSlim — a multithreading synchronization method allowing one thread to wait for another to proceed in a coordinated way. There are also other synchronization collections and classes.

For example, the Interlocked class allows thread-safe and lock-free operations but has some limitations:

- Limited operation types and variable types.
- It protects methods rather than variables — variables used elsewhere may not remain thread-safe.
- Composing thread-safe operations rarely results in overall thread safety.
- It doesn’t guarantee that the retrieved value is the most recent one.

### Async with Multithreading

You can use Task.Run(async () => { await ... }) to leverage both multithreading and async programming. When Task.Run() is called, the async method is passed to the thread pool for processing, and execution continues immediately without waiting for the async method until it reaches await or returns a Task. This is particularly useful when invoking multiple tasks before awaiting all of them — Task.Run() allows near-instant invocation.

```csharp
public async Task Process10Files() {
    var tasks = new Task[10];
    for (int i = 0; i < 10; ++i) {
        var icopy = i;
        tasks[i] = Task.Run(async () => {
            await File.ReadAllBytesAsync($"{icopy}.txt");
            Console.WriteLine("Doing something with the file's content");
        });
    }
    await Task.WhenAll(tasks);
}
```

- The continuation logic after await depends on the application type. In WF/WPF apps, it usually runs on the same UI thread due to SynchronizationContext.
- In ASP.NET Framework apps (before 4.8), continuation also runs on the same thread.
- In modern ASP.NET Core apps, continuations typically run on a thread pool thread.
    - If await is used within a manually created thread, that thread ends, and continuation runs on the thread pool.

Data used inside async/await can be accessed by multiple threads, so thread-safe data types or proper locking should be used.

Also, since Task.Run() executes on another thread, you should await Task.Run() on UI threads to ensure continuation runs on the correct UI thread.

### Multithreading Pitfalls

Concurrency is performing multiple tasks within a time frame — either interleaved or parallel.

Multithreading enables concurrency; even a single-core CPU can run multiple threads, switching between them to handle multiple tasks.

Thread safety ensures that a program’s output is consistent and predictable even when accessed by multiple threads.

A race condition occurs when the program’s output depends on the timing or order of thread execution, leading to inconsistent results.

### Pitfalls

**Partial updates**  

This can occur in multithreading environments when one thread updates shared data, but another reads it midway. The shared data ends up in a mixed state of old and new values. Use ‘lock’ for shared data among threads.

**Access memory ordering**  

This refers to instruction reordering by the compiler or CPU to optimize pipelines and execution units. Access memory refers to internal CPU operations. Each CPU instruction is measured in **clock cycles**. A 3GHz CPU performs 3 billion cycles per second. Fewer clock cycles → faster operation. Optimized memory access patterns can minimize slow operations and even skip some fast ones. The CPU computes using internal variables stored in **registers**.

```csharp
var a = 0;
for (int i = 0; i < 100; i++) {
    a += 1;
}
// Machine code like
set register to 0  // fast: inside CPU
store register to memory location "a" // slow
for (int i = 0; i < 100; i++) {
    load from memory location "a" into register // slow
    increment value // fast
    load into memory location // slow
}
// 201 slow operations, 101 fast operations

// Optimized
set register to 0
for (int i = 0; i < 100; i++) {
    increment value
}
load into memory location "a"
// 101 fast
// 1 slow
```

Thus, threads may see data changes differently. However, C# code uses *acquire semantics, release semantics, memory barriers* to ensure data consistency during CPU execution.

**Deadlock**  

Occurs when a thread gets stuck waiting for unavailable resources. A simple case: thread A holds resource A while waiting for resource B, and thread B holds resource B while waiting for resource A. Both are stuck waiting for each other.

**Race condition**  

### Lock usage rules

Use locks for shared data.

Follow consistent lock order across threads.

Use locks that cover enough logic to prevent race conditions but not so long that threads become sequential or synchronous. There’s no perfect duration; trade-offs are required — choosing between accuracy and performance.

Avoid running uncontrolled logic inside locks.

Don’t modify thread priority or processor affinity.

Composing multiple thread-safe operations rarely results in thread safety; instead, use a lock that covers those operations.

### Parallel class

Allows using the `Parallel` template to implement parallel logic instead of writing everything from scratch.

The methods most commonly used are `ForEach` and `ForEachAsync`.

When the processing of tasks is just fire-and-forget, you can use a background thread to execute the tasks without caring about the completion time, allowing the original thread to continue its other work.

You can use `BlockingCollection` or `Channel` to queue items and perform thread-safe add/take/remove operations on tasks.

Or you can use external persistent storage and choose between **at-least-once-delivery** or **at-most-once-delivery** strategies, while managing poison messages in dead-letter queues for monitoring purposes.

---

### Canceling background task

`CancellationToken` is used as a flag to check whether a program should stop by checking `CancellationToken.IsCancellationRequested`.

`CancellationTokenSource` is used to create and control a `CancellationToken`.

```csharp
var cancellationTokenSource = new CancellationTokenSource();
var cancellation = cancellationTokenSource.Token;

var thread = new Thread(BackgroundProc);
void BackgroundProc() {
    int i = 0;
    while (true) {
        if (cancellation.IsCancellationRequested) return;
        Console.WriteLine(i++);
    }
}

thread.Start();
Console.ReadKey();
cancellationTokenSource.Cancel();
```

Use CancellationToken.ThrowIfCancellationRequested() to throw an exception when IsCancellationRequested is true.

CancellationToken supports callbacks that are invoked when IsCancellationRequested is true. This can be used to cancel a Task when you cannot implement cancellation logic inside it. Use CancellationToken.Register to register a callback; the function returns an object used to manually unregister the callback.

csharp
Copy code
var callbackRegister = cancellationToken.Register(delegate);
CancellationTokenSource.CancelAfter is used to cancel an operation if it does not complete within a given period of time.

CancellationTokenSource.CreateLinkedTokenSource creates a new CancellationTokenSource that can be controlled from one or more CancellationTokens.

Await events by Task Completion Source
TaskCompletionSource<T> / TaskCompletionSource is used to manage a Task<T> / Task via the methods SetResult and SetException.

TaskCompletionSource.Task represents and manages the single Task created by it.

TaskCompletionSource.SetResult / TrySetResult
Sets the result for the task and changes the task’s state so that IsCompleted is true and IsCompletedSuccessfully is true.

TaskCompletionSource.SetException / TrySetException
Sets an exception for the task and changes the task’s state so that IsCompleted is true and IsFaulted is true.

TaskCompletionSource.SetCanceled / TrySetCanceled
Cancels the task, changing the task’s state so that IsCompleted is true and IsCanceled is true, and a TaskCanceledException is thrown. You can pass a CancellationToken when calling SetCanceled / TrySetCanceled; that token will be stored in TaskCanceledException.CancellationToken.

TaskCompletionSource is used to create and control tasks, while CancellationTokenSource (CTS) and CancellationToken (CT) manage cancellation tokens as logical signals used to decide when to terminate.

```csharp

var cancellationTokenSource = new CancellationTokenSource();
cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));
var cancellation = cancellationTokenSource.Token;

var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

var registration = cancellation.Token.Register(() =>
{
    tcs.TrySetCanceled(cancellationTokenSource.Token);
});
```
### Thread-safe Collections

You can use a `lock` for collections that are accessed (possibly concurrently) from multiple threads. However, this approach introduces several issues:

- **Locking the entire operation** (both read and write) even when there is no data → results in unnecessary lock overhead.
- **Locking individual read–then–write operations** (e.g., read first, then write if missing) does not guarantee thread-safety and may still cause race conditions.

---

### Concurrent Collections

Concurrent collections use fine-grained locking strategies and lock-free techniques to ensure thread-safety.

### `ConcurrentDictionary<TKey, TValue>`

- `TryGetValue`
- `TryAdd`
- `GetOrAdd`  
  When implementing strategies such as cache-aside:
    - You might first call `TryGetValue` to retrieve data.
    - If not found, you initialize a new value and call `TryAdd`.
    - This pattern still allows the possibility that multiple threads initialize and write data at the same time, causing one thread to override the other's data.

  `GetOrAdd` ensures that the value created by the first thread is the one used by all other threads.

- `TryRemove`
- `TryUpdate(key, newValue, oldValue)`  
  Returns `false` if the current value of the key differs from `oldValue`.  
  Otherwise, it updates the value to `newValue` and returns `true`.

---

### `BlockingCollection<T>`

- A blocking producer–consumer abstraction built on top of other concurrent collections:  
  `ConcurrentQueue<T>`, `ConcurrentStack<T>`, `ConcurrentBag<T>`.
- Supports storing and retrieving items in order.
- Supports **Add/Take until** operations: these operations block until conditions allow the operation to proceed.
- `Add` / `TryAdd` both add items into the underlying collection.  
  Both block until an item is removed; however, `TryAdd` supports a timeout.
- Similarly, `Take` / `TryTake`.
- `GetConsumingEnumerable` returns an `IEnumerable<T>` that allows `foreach` to process each item and remove it when consumed.
- `CompleteAdding` signals that no more items will be added.  
  All `GetConsumingEnumerable` loops end once existing items are consumed.
- `BlockingCollection` **does not** support `async/await`.

---

### `Channel<T>`

- Supports `async/await` and is thread-safe for **multiple producers** and **multiple consumers**.
- `Channel.Reader` is used to read items from the channel.
- `Channel.Writer` is used to write items into the channel.

---

### Other Concurrent Collections

- `ConcurrentQueue<T>`
- `ConcurrentStack<T>`

---

### Immutable Collections

- Immutable collections avoid threading issues by ensuring thread-safety through immutability:  
  whenever a write operation occurs, the thread receives a **new copy** of the collection that includes the updated value.
- Internally, immutable collections use persistent data structures that allow **structure sharing** across multiple copies.
- Immutable collection **variables** themselves may not be thread-safe if reassigned after each write operation.  
  Use `ImmutableInterlocked` with the appropriate methods to ensure thread-safe updates.
### IEnumerable, IEnumerator, IAsyncEnumerable

-   `IEnumerable` is used to access item sequences on the fly or from an
    external source without needing to load everything into memory.

-   `foreach` is the syntax that allows iterating and retrieving items
    from an `IEnumerable`:

``` csharp
foreach(var item in IEnumerable){
    Console.WriteLine(item); // item.ToString()
}
```

-   Compiler transformation:

``` csharp
using(var enumerator = IEnumerable.GetEnumerator()){
    while(enumerator.MoveNext()){
        var item = enumertor.Current;
        Console.WriteLine(item);
    }
}
```
- IEnumerable.GetEnumerator returns an IEnumerator. When a method uses yield return, the compiler automatically generates the entire iterator implementation. Instead of writing the iterator manually, the compiler rewrites the method into a state machine. In this state machine, MoveNext() advances the execution to the next yield point, and Current holds the value produced by that yield return.

``` csharp
public class TestEnumerator : IEnumerator<T>
{
    private int state;
    private T current;
    public T Current => current;
    ...
    public bool MoveNext()
    {
        switch (state)
        {
            case 0:
                current = itemCurrent;
                state = 1;
                return true;

            case 1:
                state = -1;
                break;
        }

        return false;
    }
}
```

-   `IAsyncEnumerable` is similar to `IEnumerable`, but adds syntax for
    async programming:
    -   `MoveNextAsync()` returns a `ValueTask` instead of
        `void MoveNext()`
    -   `GetAsyncEnumerator()` returns an `IAsyncEnumerator()` instead
        of `IEnumerator()`
    -   There is no direct way to pass a `CancellationToken` into
        `GetAsyncEnumerator()` (to cancel an `await foreach`), but you
        can pass it through `IAsyncEnumerable.WithCancellation(token)`
    -   However, inside `GetAsyncEnumerator()` --- since it is generated
        by the compiler --- to access the token internally you must use
        `[EnumeratorCancellation]`

``` csharp
private static async IAsyncEnumerable<int> AsyncYieldDemo(
        [EnumeratorCancellation] System.Threading.CancellationToken cancellationToken = default)
```

``` csharp
var cts = new CancellationTokenSource();
cts.Cancel();
await foreach (var current in AsyncYieldDemo(cts.Token))
```

-   But when you already have an existing `IAsyncEnumerable`:

``` csharp
var seq = AsyncYieldDemo();
var cts1 = new CancellationTokenSource();

// To use CancellationToken when doing await foreach on seq
// You can only pass it into GetEnumerator()

Task.Run(async () => {
    await foreach (var current in seq.WithCancellation(cts.Token))
    {
    }
});

await foreach (var current in seq.WithCancellation(cts1.Token))
{
}
```

-   `WithCancellation` only applies to the specific enumerator instance
    created by that call.

-   Output:


    1
    1
    Exception due to cts Cancel
    2 : because cts1 was not cancelled
