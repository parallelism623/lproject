using lproject.Data.Replication.Abstractions;
using lproject.Data.Replication.BackgroundServices;
using lproject.Data.Replication.Entities;
using lproject.Data.Replication.Kafka;
using lproject.HostConfiguration.ConfigurationAccessor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<LeaderDbContext>(o => o.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=pass"));
builder.Services.AddDbContext<FollowerDbContext>(o => o.UseNpgsql("Host=localhost;Port=5434;Database=postgres;Username=postgres;Password=pass"));
builder.Services.AddScoped<IMessagePublisher, KafkaMessagePublisher>();
builder.Services.AddHostedService<ReplicationBackgroundService>();
builder.Services.AddConfigurationAccessor(builder.Configuration);
builder.Services.Configure<KafkaConsumerSettings>(
    builder.Configuration.GetSection("Kafka:ConsumerSettings"));

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();   

app.Run();
