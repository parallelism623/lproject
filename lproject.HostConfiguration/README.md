## Overview

WebApplication used to configure the HTTP pipeline, and routes

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

Or

```csharp
var app =  WebApplication.Create(args);

app.MapGet("/", () => "Hello World!");

app.Run();
```

WebApplcationBuilder ussed to configure web application and services before creating a WebApplication instance:

- Configuration
- Environment
- Host (IHostBuilder) or WebHost (IWebHostBuilder)
- Metrics
- Services (IServiceCollection)

We can configure WebApplication in order to listen on specific urls.

```csharp
// Use app.Run('specific url')
app.Run("http://localhost:3000");

// Use multiple urls
app.Urls.Add("url1");
app.Urls.Add("url2");

// Use --urls command-line argument
'dotnet run --urls="url3"'

// Configure Kestrel environment variable.
ASPNETCORE_HTTP_PORTS=3000;5005
ASPNETCORE_HTTPS_PORTS=5000
```

Besides configuring the `WebApplication`, we can also use other services that are configured by default or through the `WebApplicationBuilder`.

```csharp
// Configuration
WebApplication.Configuration
 // to get IConfiguration
WebApplication.Logging
 // to get ILogger
using (var scope = app.Services.CreateScope()){
	scoped.ServiceProvider....
	// to get ServiceProvider
}
```

Before initilizing a WebApplication instance, we can configure WebApplicationBuilder to set the content root, application name, enviroment and other services.

```csharp
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Staging,
    WebRootPath = "customwwwroot"
});
var enviroment = builder.Enviroment; // to get Enviroment variables
var configuration = builder.Configuration; 
// to get IConfiguration that can add any other IConfigurationProviders
//...
var app = builder.Build();
```

Some property of WebApplicationOptions can be configured by correspond Enviroment variable or Command-line Arguments.

| feature | Environment variable | Command-line argument |
| --- | --- | --- |
| Application name | ASPNETCORE_APPLICATIONNAME | --applicationName |
| Environment name | ASPNETCORE_ENVIRONMENT | --environment |
| Content root | ASPNETCORE_CONTENTROOT | --contentRoot |

The `WebApplicationBuilder` uses the `HostApplicationBuilder` to get `IConfiguration`, `IServiceCollection`, and other core services. Inside the `HostApplicationBuilder`, an `IHostBuilder` is used to build and initialize these services.

## Generic Host in Web App

A generic host is an object encapsulates an app’s resources:

- Configuraiton
- Logging
- Configuration
- IHostedService implementations (background services)

*When a host starts, it call IHostedService.StartAsync() on each of the implementation of IHostedService. In web app, one of these implementations is a web service: Kestrel, IS HTTP Server…*

To create a new host:

```csharp
 **await Host.CreateDefaultBuilder(args)
     .ConfigureWebHostDefaults(webBuilder =>
	    {
	        webBuilder.UseStartup<Startup>();
	    })
    .Build()
    .RunAsync();
```

In web app, this code will be called IHostBuilder.Build() to create a instance of IHost.

The ***CreateDefaultBuilder*** method:

- Set the content root, that is returned by GetCurrentDirectory.
- Load host configuration from Enviroment Variables prefixed with DOTNET_
- Load app configuration from:
    - appsettings.json
    - appsettings.{Environment}.json
    - Use secrets
    - Environment variables
    - Command-line arguments
- Add the following logging providers
- Enables scope validation và dependency validation in development environment

The ***ConfigureWebHostDefaults*** method:

- Loads host configuration from environment variables prefixed with `ASPNETCORE_`.
- Sets Kestrel server as the web server and configures it using the app’s hosting configuration providers.
- Adds host filtering middleware
- Adds Forwarded Headers middleware if `ASPNETCORE_FORWARDEDHEADERS_ENABLED` is True

⇒ The configuration from environment variables prefixed with `ASPNETCORE_` will override those from `DOTNET_` if they correspond.

Beside user registered services, the following services are register automatically:

- IHostApplicationLifeTime

  Use to register post-startup and graceful shutdowns tasks, these logic are triggered when host start-up/shutdown.

- IHostLifeTime

  Use to control when host starts and when it stops. The last register implementation will be used. It triggers logic that is registered in IHostApplicationLifeTime

- IHostEnvironment/IWebHostEnvironment

  IHostEnvironment uses to get information:

    - ApplicationName
    - EnvironmentName
    - ContentRootPath

  IWebHostEnvironment extends IHostEnvironment by adding WebRootPath property.


### Host configuration

Host configuraiton is used to for the properties of the IHostEnviroment.

(ASPNET_ prefix is perfer than DOTNET_ prefix)

By default in the web app, the environment variable provider with prefix `DOTNET_` and command-line arguments are included by `CreateDefaultBuilder`, then the enviroment variable provider with prefix ASPNETCORE_ is added.

HostConfiguration can be configured by IHostBuilder.ConfigureHostConfiguration

### App configuration

App configuration is used for key/value pairs that are included in `IConfiguration`.

By default it is created by calling `IHostBuilder`.`ConfigureAppConfiguration`

## List host settings

Host settings can be set by Env variables provider with prefix DOTNET_ or ASPNETCORE_. In general, env variables provider with prefix that are registered in IHostBuilder: {PREFIX_}{HostSetting}

- ApplicationName
- ContentRoot
- EnviromentName
- ShutdownTimeout:represents the maximum time allowed for the application to shut down gracefully.

**Disable app configuration reload on change**

- by default, appsetting.json and it’s override will be reloaded when file changes at runtime.

## Setting for web apps

Some host settings apply only to HTTP workload, environment variables can use to configure these

*Before use IHostBuilder for infrastructure and resources, ****The IWebHostBuilder managed both the host lifecycle and web configuraton in one place.*
