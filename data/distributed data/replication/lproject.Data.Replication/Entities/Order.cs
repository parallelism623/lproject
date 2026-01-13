using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lproject.Data.Replication.Entities;

[Table("orders")]
public class Order
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("customer_email", TypeName = "text")]
    public string CustomerEmail { get; set; } = default!;

    [Column("total", TypeName = "integer")]
    public int Total { get; set; }

    [Required]
    [Column("status", TypeName = "text")]
    public string Status { get; set; } = default!;

    [Column("created_at", TypeName = "timestamptz")]
    public DateTimeOffset CreatedAt { get; set; }
}