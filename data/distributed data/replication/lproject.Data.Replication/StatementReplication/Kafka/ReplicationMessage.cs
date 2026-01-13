namespace lproject.Data.Replication.Kafka;

public class ReplicationMessage<T>
{
    public Status Status { get; set; }  
    public T Payload { get; set; }
}

public enum Status
{
    Modified,
    Deleted,
    Added
}