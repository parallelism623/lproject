namespace lproject.EFCore.Entities;

public class EntityGenerateValue
{
    public Guid Id { get; set; }
    public required string Computed { get; set; }
		
    public int Identity { get; set; }	
		
    public string? Concurrency { get; set; }		
		
    public string? DefaultValue { get; set; } 
		
    public string? RowVersion { get; set; }
		
    public string? ValueGeneratedNever { get; set; }
		
    public string? ValueGeneratedOnAdd { get; set; }
		
    public string? ValueGeneratedOnAddOrUpdate { get; set; }
		
    public string? ValueGeneratedOnUpdate { get; set; }
		
    public string? ValueGeneratedOnUpdateSometimes { get; set; }

    public override string ToString()
    {
        return $@"
                Id: {Id}
                Computed: {Computed}
                Identity: {Identity}
                Concurrency: {Concurrency}
                DefaultValue: {DefaultValue}
                RowVersion: {RowVersion}
                ValueGeneratedNever: {ValueGeneratedNever}
                ValueGeneratedOnAdd: {ValueGeneratedOnAdd}
                ValueGeneratedOnAddOrUpdate: {ValueGeneratedOnAddOrUpdate}
                ValueGeneratedOnUpdate: {ValueGeneratedOnUpdate}
                ValueGeneratedOnUpdateSometimes: {ValueGeneratedOnUpdateSometimes}
                ";
    }
}