using lproject.Mediator.Examples.cs;
using lproject.Mediator.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// IHostBuilder: Use to config and register service for app, include IHostedServices 
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMediator();
    })
    .Build();
var dispatcher = host.Services.GetRequiredService<IDispatcher>();
var testCommand = new TestQuery("Hieu");

var result = await dispatcher.DispatchAsync(testCommand);    
Console.WriteLine(result);
// IHost.RunAsync
await host.RunAsync();

