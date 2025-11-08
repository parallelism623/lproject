namespace lproject.EFCore.ConcurrencyConflictHandle;

public interface ITrackableEntity<T>
{
    public byte[] RowVersion { get; set; }
    
    public T CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public T? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
} 