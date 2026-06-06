using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OverheadCost
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string CostName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public DateTime CostDate { get; set; }

    public string AllocationMethod { get; set; } = "Equal";

    public ICollection<CostAllocation> CostAllocations { get; set; }
        = new List<CostAllocation>();

}