using lproject.EFCore;
using lproject.HealthChecks.HealthCheckPublishers;
using lproject.HostConfiguration;
using lproject.HealthChecks.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<DefaultHealthChecks>("Default", tags: ["sample"]);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = healthCheck => healthCheck.Tags.Contains("sample"); 
    options.Period = TimeSpan.FromSeconds(5);
});
builder.Services.RegisterHostedServices();
builder.Services.AddSingleton<IHealthCheckPublisher, AppHealthCheckPublisher>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/healthz")
    .RequireHost("*:6263");
app.UseAuthorization();
app.MapControllers();
app.Run("https://localhost:3000");
