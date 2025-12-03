
using lproject.Data.Replication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using Npgsql.Replication;
using NpgsqlTypes;

namespace lproject.Data.Replication.BackgroundServices;

public class CdcReplicationBackgroundService : BackgroundService
{
    private readonly LeaderDbContextSettings _leaderDbContextSettings;
    private const string _slotName = "replication-1";
    private readonly IServiceProvider _services;
    public CdcReplicationBackgroundService(IOptionsMonitor<LeaderDbContextSettings> leaderSettings,
        IServiceProvider services)
    {
        _leaderDbContextSettings = leaderSettings.CurrentValue;
        _services = services;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var repl = new LogicalReplicationConnection(_leaderDbContextSettings.ConnectionString);
            await repl.Open(stoppingToken);
            try { await repl.CreatePgOutputReplicationSlot(_slotName, temporarySlot: false, cancellationToken: stoppingToken); }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.DuplicateObject) { }


            using var scope = _services.CreateScope();
            var readDb = scope.ServiceProvider.GetRequiredService<FollowerDbContext>();
            var startLsn = LoadCheckpointLSN(readDb); 


        }


        NpgsqlLogSequenceNumber? LoadCheckpointLSN(DbContext db) 
        { 
            var lsnText = db.Database.SqlQueryRaw<string>("SELECT last_lsn FROM cdc_checkpoint WHERE id=1").FirstOrDefault(); 
            return string.IsNullOrWhiteSpace(lsnText) ? null : NpgsqlLogSequenceNumber.Parse(lsnText); 
        }
    }
}
