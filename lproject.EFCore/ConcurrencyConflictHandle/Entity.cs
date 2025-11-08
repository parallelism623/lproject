using System.ComponentModel.DataAnnotations;

namespace lproject.EFCore.ConcurrencyConflictHandle;

public abstract class Entity<T, TKey> : IEntity<T, TKey>
{
    [Timestamp]
    public byte[] RowVersion { get; set; } = default!;
    public TKey CreatedBy { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public TKey? ModifiedBy { get; set; } 
    public DateTime? ModifiedDate { get; set; }
}