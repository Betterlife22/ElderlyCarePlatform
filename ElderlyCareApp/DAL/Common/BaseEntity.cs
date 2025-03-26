namespace DAL.Common;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Created = DateTime.Now;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? CreatedBy { get; set; }
    public int? DeletedBy { get; set; }
    public int? LastModifiedBy { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
    public DateTime? DeletedTime { get; set; }

}
