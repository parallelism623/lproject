using HealthChecks.UI.Client;
using lproject.Lib.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<DefaultHealthChecks>("Default", tags: ["sample"])
    .AddRedis("localhost:6379", name: "redis");
builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = healthCheck => healthCheck.Tags.Contains("sample"); 
    options.Period = TimeSpan.FromSeconds(5);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/healthz", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();
app.MapHealthChecksUI(st =>
{

});
app.Run();
