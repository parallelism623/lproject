
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Mail;

namespace lproject.Concurrency.Multithreading;

public class WorkQueuePatternExample
{
    private record MailInfo(
         string from,
         string subject,
         string text,
         string email);
    BlockingCollection<MailInfo> _queue = new();
    public void Start()
    {
        var thread = new Thread(BackGroundProc);
        var cancelToken = new CancellationTokenSource();
        var token = cancelToken.Token.Register(Stop);

        thread.Start();
    }

    public void Stop()
    {
        _queue.CompleteAdding();
    }

    public void MailMerge(
        string from,
        string subject,
        string text,
        (string email, string name)[] recipients)
    {
        foreach (var current in recipients)
        {
            _queue.Add(new MailInfo(
            from,
            subject,
            text.Replace("{name}", current.name),
            current.email));
        }
    }
    public void BackGroundProc()
    {
        
        foreach (var current in _queue.GetConsumingEnumerable())
        {
            try
            {
                SendMail();
            }
            catch
            {
                LogFailure(current.email);
            }
        }
    }

    public void SendMail() => Console.WriteLine("Send....");
    private void LogFailure(string message)
    {

    }
}
